using System;

namespace CV19.Infrastructure.Commands
{
    public class LambdaCommand : Commands.Base.CommandBase
    {
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;

        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public override void Execute(object parameter) => _Execute(parameter);


        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;
    }
}