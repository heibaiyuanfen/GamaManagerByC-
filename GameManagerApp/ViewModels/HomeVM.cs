// 引入必要的命名空间
using GameManagerApp.Utilites; // 包含工具类，例如RelayCommand
using Microsoft.Win32; // 用于访问OpenFileDialog，打开文件对话框
using System.Collections.ObjectModel; // 支持数据绑定的集合类型
using System.IO; // 提供对文件系统的操作
using System.Windows.Input; // ICommand接口所在命名空间
using System.Diagnostics; // 提供访问系统进程的类
using System.Drawing; // 处理图像和图标
using System.Windows; // WPF的基本类，例如MessageBox
using GameManagerApp.Models;
using Microsoft.EntityFrameworkCore;
using GameManagerApp.Repository;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GameManagerApp.IRepository;
using GameManagerApp.Pool;


namespace GameManagerApp.ViewModels
{
    // HomeVM类继承自ViewModelBase，提供了属性更改通知的基础结构
    class HomeVM : ViewModelBase
    {


        private GameProcessPool _processPool = new GameProcessPool();

        private IGameInfoRepository _gameInfoRepository;


        // 私有字段，用于存储当前显示的视图模型
        private object _GameInfoView;

        public object CurrentView
        {
            get { return _GameInfoView; }
            set { _GameInfoView = value; OnPropertyChanged(); }
        }

        // Games集合存储游戏模型，支持UI自动更新
        public ObservableCollection<GameModel> Games { get; private set; } = new ObservableCollection<GameModel>();

        // SelectedGame属性表示当前选中的游戏
        public GameModel SelectedGame { get; set; }

        // 命令属性，用于在UI中绑定对应的操作
        public ICommand ScanDiskCommand { get; set; }
        public ICommand AddGameCommand { get; set; }
        public ICommand OpenGameCommand { get; set; }

        //游戏信息界面的指令，不知道是否可以用上，先写上
        public ICommand GameInfoCommand { get; private set; }



        private void GameInfo(object obj) => CurrentView = new GameInfoVM();


        // 构造函数中初始化命令
        public HomeVM()
        {
            // 初始化命令，绑定相应的操作
            AddGameCommand = new RelayCommand(_ => AddGame());
            OpenGameCommand = new RelayCommand(game => OpenGame());
            GameInfoCommand = new RelayCommandforGameInfo<GameModel>(ShowGameInfo);
            _processPool.AddExistingProcessesAsync();
            LoadGames();
        }


        private async void LoadGames()
        {

            _gameInfoRepository = new GameInfoRepository();
            try
            {
                var games = await _gameInfoRepository.GetAllAsync();
                foreach (var gameInfo in games)
                {

                    var gameIcon = Icon.ExtractAssociatedIcon(gameInfo.FilePath);
                    // 可以根据需要将Icon转换为ImageSource
                    // 此处示例仅直接使用FilePath
                    var gameModel = new GameModel
                    {
                        Name = gameInfo.Name,
                        FilePath = gameInfo.FilePath,
                        // 假设我们有方法将存储的图标数据转换为ImageSource
                        GameIcon = gameIcon
                       
                    };
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Games.Add(gameModel);
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载游戏时出错: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void ShowGameInfo(GameModel game)
        {
            // 这里实现你想要执行的逻辑，比如显示游戏详情
            // 示例：MessageBox.Show($"展示游戏信息：{game.Name}");
            MessageBox.Show(game.Name);

            var gameInfo = new GameInfo
            {
                FilePath = game.FilePath,
                Name = game.Name,
            };

            CurrentView = new GameInfoVM(gameInfo);
        }



        // AddGame方法用于添加游戏
        // 异步的AddGame方法
        private async Task AddGame()
        {

            _gameInfoRepository = new GameInfoRepository();

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "游戏文件 (*.exe)|*.exe" // 设置文件过滤器
            };

            if (openFileDialog.ShowDialog() == true) // 如果用户选择了文件
            {
                string selectedFilePath = openFileDialog.FileName;
                string fileName = Path.GetFileNameWithoutExtension(selectedFilePath);
                
                // 在数据库中检查游戏是否存在
                var existingGame = await _gameInfoRepository.GetByNameAsync(fileName);
                if (existingGame != null)
                {

                    // 如果游戏已存在，从数据库获取信息并提取图标
                    // ...
                    MessageBox.Show($"游戏已经存在: {existingGame.Name}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    // 提取图标
                    var gameIcon = Icon.ExtractAssociatedIcon(selectedFilePath);

                    // 创建游戏信息对象
                    var gameInfo = new GameInfo
                    {
                        Name = fileName,
                        FilePath = selectedFilePath,
                        // 根据实际情况可能需要存储图标数据

                    };

                    // 将游戏信息存入数据库
                    await _gameInfoRepository.Add(gameInfo);

                    // 创建新的 GameModel 实例并添加到 Games 集合以更新 UI
                    var gameModel = new GameModel
                    {
                        Name = fileName,
                        FilePath = selectedFilePath,
                        GameIcon = gameIcon
                    };

                    Games.Add(gameModel);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"添加游戏时出错: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }








        private async void OpenGame()
        {
            if (SelectedGame != null && !string.IsNullOrEmpty(SelectedGame.FilePath))
            {
                // 检查游戏进程是否已存在
                var gameProcess = _processPool.RentProcess(SelectedGame.FilePath);

                // 如果进程已经存在且没有退出，尝试将其窗口带到前台
                if (gameProcess != null && gameProcess.Process != null && !gameProcess.Process.HasExited)
                {
                    MessageBox.Show("游戏已在之前启动。", "信息", MessageBoxButton.OK, MessageBoxImage.Information);
                    // 将游戏窗口带到前台的代码应该在这里实现
                    // 例如：WinApi.BringProcessToFront(gameProcess.Process.Id);
                    return;
                }

                // 尝试启动游戏
                try
                {
                    // 创建并启动游戏进程
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo(SelectedGame.FilePath) { UseShellExecute = true },
                        EnableRaisingEvents = true // 允许使用Exited事件
                    };

                    process.Start();
                    // 记录启动的游戏进程
                    //_processPool.AddProcess(SelectedGame.FilePath, process);

                    process.Exited += (sender, args) =>
                    {
                        // 当游戏退出时，执行归还和清理操作
                        _processPool.ReturnProcess(SelectedGame.FilePath);
                        process.Dispose();
                    };
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"无法启动游戏: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }






        private void SelectGameInfo()
        {

        }

        // DetermineImagePathForGame方法用于根据游戏路径获取游戏图片路径
        // 这里需要你根据实际情况实现具体逻辑
        private string DetermineImagePathForGame(string gamePath)
        {
            return "YourLogicHere";
        }
    }
}
