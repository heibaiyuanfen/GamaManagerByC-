using GameManagerApp.IRepository;
using GameManagerApp.Models;
using GameManagerApp.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GameManagerApp.Pool
{
    public class GameProcessPool
    {
        private readonly Dictionary<string, GameProcess> _pool = new Dictionary<string, GameProcess>();

        private readonly IGameInfoRepository _gameInfoRepository  = new GameInfoRepository(); // 假设您有一个游戏信息仓库


        // 添加现有的进程到池中
        public async Task AddExistingProcessesAsync()
        {
            var allProcesses = Process.GetProcesses();
            var gameInfos = await _gameInfoRepository.GetAllAsync(); // 获取所有游戏信息

            foreach (var gameInfo in gameInfos)
            {
                var matchingProcess = allProcesses.FirstOrDefault(
                    p => GetProcessExecutablePath(p)?.Equals(gameInfo.FilePath, StringComparison.OrdinalIgnoreCase) == true
                );

                if (matchingProcess != null)
                {
                    _pool[gameInfo.FilePath] = new GameProcess
                    {
                        Process = matchingProcess,
                        StartTime = DateTime.Now // 记录当前时间作为开始时间
                    };
                }
            }
        }

        // 获取进程的可执行文件路径，返回null如果无法获取
        private string GetProcessExecutablePath(Process process)
        {
            try
            {
                return process.MainModule.FileName;
            }
            catch
            {
                return null; // 无法访问进程
            }
        }

        // “借出”进程
        public GameProcess RentProcess(string gameFilePath)
        {
            // 检查进程是否已存在
            if (_pool.TryGetValue(gameFilePath, out var gameProcess) && gameProcess.Process != null && !gameProcess.Process.HasExited)
            {
                // 如果已经有一个进程正在运行，则返回现有的进程
                return gameProcess;
            }

            // 如果不存在，则创建新的进程
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(gameFilePath) { UseShellExecute = true },
                EnableRaisingEvents = true
            };
            process.Start();

            gameProcess = new GameProcess
            {
                Process = process,
                StartTime = DateTime.Now // 记录当前时间作为开始时间
            };

            _pool[gameFilePath] = gameProcess;

            // 监听进程退出事件
            process.Exited += (sender, e) =>
            {
                this.ReturnProcess(gameFilePath); // 当进程结束时，“归还”
            };

            return gameProcess;
        }

        // “归还”进程
        public void ReturnProcess(string gameFilePath)
        {
            if (_pool.TryGetValue(gameFilePath, out var gameProcess))
            {
                gameProcess.EndTime = DateTime.Now; // 记录当前时间作为结束时间
                // 计算运行时间
                var runningTime = gameProcess.EndTime.Value - gameProcess.StartTime;
                gameProcess.RunningTime = runningTime;

                // TODO: 在这里记录结束时间和运行时间，可以更新数据库或进行其他操作
                // ...

                // 进程已结束，可以从池中移除
                _pool.Remove(gameFilePath);
            }
        }
    }
}
