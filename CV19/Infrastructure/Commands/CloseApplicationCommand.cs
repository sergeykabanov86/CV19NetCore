namespace CV19.Infrastructure.Commands
{
    public class CloseApplicationCommand : CV19.Infrastructure.Commands.Base.CommandBase
    {

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
