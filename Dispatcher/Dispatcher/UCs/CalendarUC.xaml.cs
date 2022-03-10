using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dispatcher.UCs
{
    /// <summary>
    /// Логика взаимодействия для CalendarUC.xaml
    /// </summary>
    public partial class CalendarUC : UserControl
    {
        DateTime? FirstDate;
        DateTime? SecondDate;
        public CalendarUC()
        {
            InitializeComponent();
        }

        private void FirstDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Calendar.DisplayDateStart = FirstDatePicker.SelectedDate;
            FirstDate = FirstDatePicker.SelectedDate;
        }

        private void SecondDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Calendar.DisplayDateEnd = SecondDatePicker.SelectedDate;
            SecondDate = SecondDatePicker.SelectedDate;
        }

        private void Default_Click(object sender, RoutedEventArgs e)
        {
            FirstDatePicker.SelectedDate = null;
            SecondDatePicker.SelectedDate = null;
            FirstDate = null;
            SecondDate = null;
        }
    }
}
