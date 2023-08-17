using System.Windows.Input;

namespace WPF.Common.Mvvm
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other objects by
    /// invoking delegates. The default return value for the CanExecute method is true.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object>? _canExecute;

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="action">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> action, Predicate<object>? canExecute = null)
        {
            this._action = action ?? throw new ArgumentNullException(nameof(action));

            if (canExecute != null)
            {
                this._canExecute = canExecute;
            }
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="action">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action action, Func<bool>? canExecute = null)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            this._action = obj => action();
            if (canExecute != null)
            {
                this._canExecute = obj => canExecute();
            }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require
        /// data to be passed, this object can be set to null.
        /// </param>
        /// <returns>True if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter!) ?? true;

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require
        /// data to be passed, this object can be set to null.
        /// </param>
        public void Execute(object? parameter)
        {
            _action(parameter!);
        }
    }

    /// <summary>
    /// Generic implementation of relay command. 
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T>? myCanExecute;
        private readonly Action<T> myExecute;

        public RelayCommand(Action<T> theExecute)
            : this(theExecute, null)
        {
            myExecute = theExecute ?? throw new ArgumentNullException(nameof(theExecute));
        }

        public RelayCommand(Action<T> theExecute, Predicate<T>? theCanExecute)
        {
            myExecute = theExecute ?? throw new ArgumentNullException(nameof(theExecute));
            myCanExecute = theCanExecute;
        }

        public bool CanExecute(object? theParameter)
        {
            return myCanExecute == null || myCanExecute((T)theParameter!);
        }

        public void Execute(object? theParameter)
        {
            myExecute((T)theParameter!);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
