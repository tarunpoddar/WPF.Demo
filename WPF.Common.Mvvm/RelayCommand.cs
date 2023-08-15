using System.Windows.Input;

namespace WPF.Common.Mvvm
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly Action<object>? action;
        private readonly Predicate<object>? canExecute;
        private Action<string> selectSuggestion;

        public RelayCommand(Action action, Func<bool>? canExecute = null)
        {
            this.action = action != null ? (Action<object>)(obj => action()) : throw new ArgumentNullException(nameof(action));
            if (canExecute == null)
                return;
            this.canExecute = (Predicate<object>)(obj => canExecute());
        }

        public bool CanExecute(object? parameter)
        {
            Predicate<object> canExecute = this.canExecute;
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter) => this.action(parameter);
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute)
           : this(execute, null)
        {
            _execute = execute;
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
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

        public event EventHandler? CanExecuteChanged;

        //public event EventHandler CanExecuteChanged
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}
    }
}
