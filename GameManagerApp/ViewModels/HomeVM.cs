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

namespace GameManagerApp.ViewModels
{
    // HomeVM类继承自ViewModelBase，提供了属性更改通知的基础结构
    class HomeVM : ViewModelBase
    {
        // Games集合存储游戏模型，支持UI自动更新
        public ObservableCollection<GameModel> Games { get; private set; } = new ObservableCollection<GameModel>();

        // SelectedGame属性表示当前选中的游戏
        public GameModel SelectedGame { get; set; }

        // 命令属性，用于在UI中绑定对应的操作
        public ICommand ScanDiskCommand { get; set; }
        public ICommand AddGameCommand { get; set; }
        public ICommand OpenGameCommand { get; set; }

        // 构造函数中初始化命令
        public HomeVM()
        {
            // 初始化命令，绑定相应的操作
            AddGameCommand = new RelayCommand(_ => AddGame());
            OpenGameCommand = new RelayCommand(game => OpenGame());
        }

        // AddGame方法用于添加游戏
        private void AddGame()
        {
            // 打开文件对话框让用户选择游戏文件
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "游戏文件 (*.exe)|*.exe"; // 设置文件过滤器
            if (openFileDialog.ShowDialog() == true) // 如果用户选择了文件
            {
                string selectedFilePath = openFileDialog.FileName;

                // 检查所选游戏是否已存在
                if (Games.Any(game => game.FilePath.Equals(selectedFilePath, StringComparison.OrdinalIgnoreCase)))
                {
                    // 如果游戏已存在，则显示错误消息
                    MessageBox.Show("此游戏已存在！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // 创建新的GameModel实例并添加到Games集合
                var game = new GameModel
                {
                    Name = Path.GetFileNameWithoutExtension(selectedFilePath),
                    FilePath = selectedFilePath,
                };
                Games.Add(game);
            }
        }

        // OpenGame方法用于打开选中的游戏
        private void OpenGame()
        {
            if (SelectedGame != null && !string.IsNullOrEmpty(SelectedGame.FilePath))
            {
                try
                {
                    // 根据游戏文件路径启动游戏
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(SelectedGame.FilePath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    // 异常处理逻辑，例如显示错误消息
                }
            }
        }

        // DetermineImagePathForGame方法用于根据游戏路径获取游戏图片路径
        // 这里需要你根据实际情况实现具体逻辑
        private string DetermineImagePathForGame(string gamePath)
        {
            return "YourLogicHere";
        }
    }
}
