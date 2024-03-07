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


namespace GameManagerApp.ViewModels
{
    // HomeVM类继承自ViewModelBase，提供了属性更改通知的基础结构
    class HomeVM : ViewModelBase
    {


        private readonly GameInfoRepository _gameInfoRepository;


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
        }


        private void ShowGameInfo(GameModel game)
        {
            // 这里实现你想要执行的逻辑，比如显示游戏详情
            // 示例：MessageBox.Show($"展示游戏信息：{game.Name}");
            MessageBox.Show(game.Name);
            CurrentView = new GameInfoVM();
        }



        // AddGame方法用于添加游戏
        // 异步的AddGame方法
        private async Task AddGame()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "游戏文件 (*.exe)|*.exe" // 设置文件过滤器
            };

            if (openFileDialog.ShowDialog() == true) // 如果用户选择了文件
            {
                string selectedFilePath = openFileDialog.FileName;
                string fileName = Path.GetFileNameWithoutExtension(selectedFilePath);

                // 检查所选游戏是否已存在
                if (Games.Any(game => game.FilePath.Equals(selectedFilePath, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("此游戏已存在！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    // 提取图标并转换为字节数组
                    var iconBytes = File.ReadAllBytes(openFileDialog.FileName); // 这里假设图标文件与exe同名
                                                                                // 创建新的GameInfo实例
                    var gameInfo = new GameInfo
                    {
                        Name = fileName,
                        FilePath = selectedFilePath,
                        Icon = iconBytes
                    };

                    // 调用仓储方法添加游戏信息到数据库
                    await _gameInfoRepository.Add(gameInfo);


                    
                    // 创建新的GameModel实例并添加到Games集合以更新UI
                    var gameModel = new GameModel
                    {
                        Name = fileName,
                        FilePath = selectedFilePath,
                        // 这里需要实现Icon转换为ImageSource的逻辑
                        GameIcon = ConvertBytesToIcon(iconBytes)
                    };

                    Games.Add(gameModel);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"添加游戏时出错: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private Icon ConvertBytesToIcon(byte[] iconBytes)
        {
            if (iconBytes == null || iconBytes.Length == 0)
                return null;

            using (var stream = new MemoryStream(iconBytes))
            {
                return new Icon(stream);
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


         private  void SelectGameInfo()
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
