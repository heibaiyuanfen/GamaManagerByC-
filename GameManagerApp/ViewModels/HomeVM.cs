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

        private IGameInfoRepository _gameInfoRepository = new GameInfoRepository();


        // 私有字段，用于存储当前显示的视图模型
        private object _GameInfoView;

        public object CurrentView
        {
            get { return _GameInfoView; }
            set { _GameInfoView = value; OnPropertyChanged(); }
        }

        // Games集合存储游戏模型，支持UI自动更新
        private static bool _isDataLoaded = false;
        private static ObservableCollection<GameModel> _games = new ObservableCollection<GameModel>();
        public ObservableCollection<GameModel> Games
        {
            get { return _games; }
            set { _games = value; OnPropertyChanged(); }
        }

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
            AddGameCommand = new AsyncRelayCommand(async () => await AddGame());
            OpenGameCommand = new RelayCommand(game => OpenGame());
            GameInfoCommand = new RelayCommandforGameInfo<GameModel>(ShowGameInfo);

            if (!_isDataLoaded)
            {
                InitializeAsync();
                _isDataLoaded = true;
            }


        }

        private async void InitializeAsync()
        {
            await _processPool.AddExistingProcessesAsync();
            await LoadGames();
        }

        private async Task LoadGames()
        {

            
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



        private async void ShowGameInfo(GameModel game)
        {
            // 这里实现你想要执行的逻辑，比如显示游戏详情
            // 示例：MessageBox.Show($"展示游戏信息：{game.Name}");
           

            GameInfo gameInfo = await _gameInfoRepository.GetGameInfoAsync(game.FilePath);



            CurrentView = new GameInfoVM(gameInfo);
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
                var gameProcess = await _processPool.RentProcessAsync(SelectedGame.FilePath);
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
