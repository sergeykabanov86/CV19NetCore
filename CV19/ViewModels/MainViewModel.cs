using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using CommonLibrary.ViewModels.Base;
using CommonLibrary.Infrastructure.Commands;

using CV19.Models;
using CV19.Models.Decanat;

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



        #region Students

        public ObservableCollection<Group> Groups { get; }

        #region GroupSelected - Выбранная группа
        private Group _GroupSelected;
        public Group GroupSelected { get => _GroupSelected; set => SetProperty(ref _GroupSelected, value); }
        #endregion GroupSelected



        #endregion Students

        public ObservableCollection<object> CompositeCollection { get;}

        #region object CompositeCollectionSelected - Selected Item

        private object _CompositeCollectionSelected;

        public object CompositeCollectionSelected
        {
            get => _CompositeCollectionSelected;
            set => SetProperty(ref _CompositeCollectionSelected, value);
        }

        #endregion object _CompositeCollectionSelected - Selected Item      
      
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
            var groups = Enumerable.Range(1, 20)
                                   .Select(i => new Group
                                   {
                                       Name = $"Группа {i}",
                                       Students = Enumerable.Range(1, 10)
                                     .Select(i => new Student
                                     {
                                         Name = $"Name: {student_idx}",
                                         Surname = $"Surname: {student_idx}",
                                         Patronomic = $"Patronomic: {student_idx++}",
                                         Birthday = DateTime.Now.AddDays(i * (-1) + 1),
                                         Rating = 0
                                     }).ToArray()
                                   });

            Groups = new ObservableCollection<Group>(groups);
            #endregion Students

            #region CompositeCollection

            var compositesList = new List<object>();
            compositesList.Add(3216);
            compositesList.Add("This a string");
            compositesList.Add(Groups[0]);
            compositesList.Add(Groups[3].Students.ElementAt(3).Birthday);
            CompositeCollection = new ObservableCollection<object>(compositesList);

            #endregion CompositeCollection
        }

        #endregion Constructors



        #region Methods

        #endregion Methods 

    }
}
