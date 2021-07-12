using System;
using System.Windows.Input;

namespace ClientWpf
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> commandHandler;
        private readonly Func<object, bool> canExecuteHandler;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> commandHandler, Func<object, bool> canExecuteHandle = null)
        {
            this.commandHandler = commandHandler;
            this.canExecuteHandler = canExecuteHandle;
        }
        public RelayCommand(Action commandHandle, Func<bool> canExecuteHandler = null)
            : this(_ => commandHandle(), canExecuteHandler == null ? null : new Func<object, bool>(_ => canExecuteHandler()))
        { }

        public bool CanExecute(object parameter)
        {
            return canExecuteHandler == null || canExecuteHandler(parameter);
        }

        public void Execute(object parameter)
        {
            commandHandler(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }

    public class RelayCommand<T> : RelayCommand
    {
        public RelayCommand(Action<T> commandHandler, Func<T, bool> canExecuteHandle = null)
            : base(o => commandHandler(o is T t ? t : default(T)), canExecuteHandle == null ? null : new Func<object, bool>(o => canExecuteHandle(o is T t ? t : default(T))))
        {
        }
    }
}
