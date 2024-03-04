// 引入所需的命名空间
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManagerApp.Utilites; // 引用工具类，例如RelayCommand
using System.Windows.Input; // 引用命令接口ICommand
using GameManagerApp.ViewModels; // 引用视图模型

namespace GameManagerApp.ViewModels
{
    // NavigationVM类继承自ViewModelBase
    class NavigationVM : ViewModelBase
    {
        // 私有字段，用于存储当前显示的视图模型
        private object _currentView;

        // 公共属性，用于获取和设置当前视图模型，当设置时触发属性更改通知
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        // 定义一系列命令，用于在UI中触发导航操作
        public ICommand HomeCommand { get; set; }
        public ICommand CustomersCommand { get; set; }
        public ICommand ProductsCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand TransacionsCommand { get; set; }
        public ICommand ShipmentsCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        // 私有方法，用于响应各导航命令，设置CurrentView为对应的视图模型实例
        private void Home(object obj) => CurrentView = new HomeVM();
        private void Custmoer(object obj) => CurrentView = new CustomerVM();
        private void Product(object obj) => CurrentView = new Products();
        private void Order(object obj) => CurrentView = new OrdersVM();
        private void Transactions(object obj) => CurrentView = new Transactinos();
        private void Shipments(object obj) => CurrentView = new Shipments();
        private void Settings(object obj) => CurrentView = new SettingVM();

        // 构造函数
        public NavigationVM()
        {
            // 初始化各导航命令，绑定到相应的私有方法
            HomeCommand = new RelayCommand(Home);
            CustomersCommand = new RelayCommand(Custmoer);
            ProductsCommand = new RelayCommand(Product);
            OrdersCommand = new RelayCommand(Order);
            TransacionsCommand = new RelayCommand(Transactions);
            ShipmentsCommand = new RelayCommand(Shipments);
            SettingsCommand = new RelayCommand(Settings);

            // 设置启动时显示的视图模型
            CurrentView = new HomeVM();
        }
    }
}


//在NavigationVM构造函数中，为每个导航项初始化了一个RelayCommand对象，并将其绑定到对应的处理方法。这些命令对象被绑定到UI中的按钮或其他触发器上。当用户与这些触发器交互时（如点击按钮），相应的处理方法被调用，CurrentView属性被更新为新的视图模型实例，从而改变应用程序中当前显示的视图。

//通过这种方式，NavigationVM提供了一种灵活的导航机制，使得应用程序能够根据用户的操作动态更改显示的内容，而不需要硬编码UI逻辑到视图模型中，从而保持了代码的清晰和可维护性。
