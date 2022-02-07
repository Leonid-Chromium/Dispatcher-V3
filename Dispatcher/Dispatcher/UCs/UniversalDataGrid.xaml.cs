using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для UniversalDataGrid.xaml
    /// </summary>
    public partial class UniversalDataGrid : UserControl
    {
        public UniversalDataGrid()
        {
            InitializeComponent();
        }

        private void MyDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Trace.WriteLine("MyDataGrid_BeginningEdit");
        }

        private void MyDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Trace.WriteLine("MyDataGrid_CellEditEnding");
        }

        private void MyDataGrid_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            Trace.WriteLine("MyDataGrid_PreparingCellForEdit");
        }

        private void MyDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Trace.WriteLine("MyDataGrid_RowEditEnding");
        }

        private void MyDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            Trace.WriteLine("MyDataGrid_AddingNewItem");
        }
    }
}
