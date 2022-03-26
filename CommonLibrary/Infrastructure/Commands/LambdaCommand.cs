using System;

namespace CommonLibrary.Infrastructure.Commands
{
    public class LambdaCommand : Commands.Base.CommandBase
    {
        Action<object> _Execute;
        Func<object, bool> _CanExecute;

        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public override void Execute(object parameter) => _Execute(parameter);


        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

       
    }
}
