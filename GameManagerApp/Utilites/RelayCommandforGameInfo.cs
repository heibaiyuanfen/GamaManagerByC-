using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input; // ICommand接口所在的命名空间
using System.Threading.Tasks;

namespace GameManagerApp.Utilites
{
    public class RelayCommandforGameInfo<T> :ICommand
    {

        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommandforGameInfo(Action<T> execute, Predicate<T> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }


    }
}
