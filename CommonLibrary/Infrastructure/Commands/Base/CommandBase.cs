using System;
using System.Windows.Input;

namespace CommonLibrary.Infrastructure.Commands.Base
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => System.Windows.Input.CommandManager.RequerySuggested += value;
            remove => System.Windows.Input.CommandManager.RequerySuggested -= value;
        }


        public abstract void Execute(object parameter);

        public abstract bool CanExecute(object parameter);

       
    }
}
