using System;
using System.Windows.Input;

namespace WPF.Common.Mvvm
{
    /// <summary>
    /// Implementation of relay command. 
    /// </summary>
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly Action<object>? myAction;
        private readonly Predicate<object>? myCanExecute;

        public RelayCommand(Action theAction, Func<bool>? theCanExecute = null)
        {
            myAction = theAction != null ? (obj => theAction()) : throw new ArgumentNullException(nameof(theAction));
            if (theCanExecute != null)
            {
                myCanExecute = obj => theCanExecute();
            }
        }

        public bool CanExecute(object? theParameter)
        {
            if (theParameter != null)
            {
                Predicate<object>? aCanExecute = myCanExecute;
                return aCanExecute == null || aCanExecute(theParameter);
            }
            return false;
        }

        public void Execute(object? theParameter)
        {
            if (theParameter != null && myAction != null)
            {
                myAction(theParameter);
            }
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
