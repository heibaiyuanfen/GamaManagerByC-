using GameManagerApp.Models;
using GameManagerApp.Repository;
using GameManagerApp.IRepository;

namespace GameManagerApp.ViewModels
{
    class GameInfoVM :Utilites.ViewModelBase
    {
        private readonly PageModel _PageModel; // 声明一个私有只读字段_PageModel，类型为PageModel。
                                               // 这个字段用于在Customers视图模型中持有一个模型实例。
        private string _runningTime;

        public string RunningTime
        {
            get => _runningTime;
            set
            {
                if (_runningTime != value)
                {
                    _runningTime = value;
                    OnPropertyChanged(nameof(RunningTime)); // 通知视图属性已更改
                }
            }
        }

        private IGameInfoRepository gameInfoRepository = new GameInfoRepository();

        public GameInfoVM()
        {
            _PageModel = new PageModel();
        }

        public GameInfoVM(GameInfo game)
        {
            _PageModel = new PageModel();
            RunningTime = game.runningtime;
        }


    }
}
