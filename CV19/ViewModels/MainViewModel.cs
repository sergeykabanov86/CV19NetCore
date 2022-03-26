using System;
using System.Collections.Generic;
using System.Text;

using CommonLibrary.ViewModels.Base;
using CommonLibrary.Infrastructure.Commands;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainViewModel : ViewModel
    {

        #region Properties

        #region WndTitle - Заголовок окна
        private string _WndTitle = "CV19";
        public string WndTitle { get => _WndTitle; set => SetProperty(ref _WndTitle, value); }
        #endregion WndTitle



        #endregion Properties



        #region Commands

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }
        private void OnCloseApplicationCommandExecuted(object p)
        {
            //WndTitle = "Second Title";
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            System.Windows.Application.Current.Shutdown();
        }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        #endregion




        #endregion Commands



        #region Constructors

        public MainViewModel()
        {

            #region CommandsInit
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            #endregion CommandsInit

        }



        #endregion Constructors



        #region Methods




        #endregion Methods

    }
}
