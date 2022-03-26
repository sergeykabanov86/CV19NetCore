using System;
using System.Collections.Generic;
using System.Windows.Input;

using CommonLibrary.ViewModels.Base;
using CommonLibrary.Infrastructure.Commands;

using CV19.Models;

namespace CV19.ViewModels
{
    internal class MainViewModel : ViewModel
    {

        #region Properties

        #region WndTitle - Заголовок окна
        private string _WndTitle = "CV19";
        public string WndTitle { get => _WndTitle; set => SetProperty(ref _WndTitle, value); }
        #endregion WndTitle

        #region DataPoints - Test Data for OxyPlot 
        private IEnumerable<DataPoint> _DataPoints;
        public IEnumerable<DataPoint> DataPoints { get => _DataPoints; set => SetProperty(ref _DataPoints, value); }
        #endregion DataPoints

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

            var data_points = new List<DataPoint>((int)(360/0.1));
            for (var x = 0d; x < 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            DataPoints = data_points;
        }
        #endregion Constructors



        #region Methods

        #endregion Methods 

    }
}
