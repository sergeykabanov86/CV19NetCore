using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CV19.Models.Decanat;

namespace CV19.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void GroupsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            var filterText = tbGroupFIlter.Text;
            if (!(e.Item is Group group)) return;
            if (group.Name is null) return;
            if (string.IsNullOrEmpty(filterText)) return;


            if (group.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;

            if (group.Description != null && group.Description.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;

        }

        private void TbGroupFIlter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            var collection = (CollectionViewSource)tb.FindResource("GroupsCollection");
            collection?.View.Refresh();
        }
    }
}
