// 引入所需的命名空间
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input; // ICommand接口所在的命名空间
using System.Threading.Tasks;

namespace GameManagerApp.Utilites
{
    // RelayCommand类实现了ICommand接口，提供了一种简单的方式来创建命令。
    class RelayCommand : ICommand
    {
        // 私有字段，保存命令的执行逻辑，一个Action<object>委托。
        private readonly Action<object> _execute;

        // 私有字段，保存命令是否可以执行的逻辑，一个返回bool的Func<object, bool>委托。
        private readonly Func<object, bool> _canExecute;

        // ICommand接口的事件，当命令的执行状态可能改变时，通知界面更新。
        // 使用CommandManager.RequerySuggested事件，自动监听命令是否可执行状态的变化。
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // RelayCommand的构造函数，需要传入执行逻辑和可执行逻辑。
        // canExecute参数是可选的，默认为null，表示命令总是可执行。
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute; // 设置命令的执行逻辑。
            _canExecute = canExecute; // 设置命令是否可以执行的逻辑。
        }

        // CanExecute方法决定命令是否可以执行。
        // 如果_canExecute为null，则命令总是可执行；否则，取决于_canExecute委托的返回值。
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        // Execute方法执行命令的逻辑，调用_execute委托。
        public void Execute(object parameter) => _execute(parameter);
    }
}


//RelayCommand提供了一种灵活的方法来将命令逻辑直接绑定到视图模型的属性或方法，而不需要在代码后置中编写事件处理逻辑。这种方式简化了视图和视图模型之间的交互，是实现MVVM模式的常用手段之一。通过CanExecute方法和CanExecuteChanged事件，它还支持根据应用程序状态启用或禁用命令，提供了对用户界面的更细粒度控制。





