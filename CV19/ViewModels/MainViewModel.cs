using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

using CommonLibrary.ViewModels.Base;
using CommonLibrary.Infrastructure.Commands;

using CV19.Models;
using CV19.Models.Decanat;

namespace CV19.ViewModels
{
    internal class MainViewModel : ViewModel
    {

        #region Students

        public ObservableCollection<Group> Groups { get; }

        #endregion

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

            #region OxyPlot
            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x < 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }
            DataPoints = data_points;
            #endregion OxyPlot

            #region Students

            var student_idx = 0;
            var students = Enumerable.Range(1, 10)
                                     .Select(i => new Student
                                     {
                                          Name = $"Name: {student_idx}",
                                           SurName = $"Surname: {student_idx}",
                                            Patronomic = $"Patronomic: {student_idx++}",
                                             Birthday = DateTime.Now.AddDays(i*(-1) + 1),
                                              Rating = 0
                                     }).ToArray();

            var groups = Enumerable.Range(1, 20)
                                   .Select(i => new Group
                                   {
                                       Name = $"Группа {i}",
                                       Students = students
                                   });

            Groups = new ObservableCollection<Group>(groups);


            #endregion Students
        }
        #endregion Constructors



        #region Methods

        #endregion Methods 

    }
}
