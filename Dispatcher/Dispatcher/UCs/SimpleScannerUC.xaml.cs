using System;
using System.Collections.Generic;
using System.Data;
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
using SQLLib;
//Увеличить шрифт внутри textbox

namespace Dispatcher.UCs
{
    /// <summary>
    /// Логика взаимодействия для SimpleScannerUC.xaml
    /// </summary>
    public partial class SimpleScannerUC : UserControl
    {
        public SimpleScannerUC()
        {
            InitializeComponent();
            SPScanner.Visibility = Visibility.Hidden;
            GridInfo.Visibility = Visibility.Hidden;
            btn_Reset.Visibility = Visibility.Hidden;
            btn_Confirm.Visibility = Visibility.Hidden;
        }

        private void Rb_Click(object sender, RoutedEventArgs e)
        {

            if (RbIn.IsChecked == true || RbOut.IsChecked == true)
            {
                SPScanner.Visibility = Visibility.Visible;
                GridInfo.Visibility = Visibility.Visible;
                btn_Reset.Visibility = Visibility.Visible;
                btn_Confirm.Visibility = Visibility.Visible;

                if (RbIn.IsChecked == true)
                {
                    RbOut.Opacity = 0.5;
                    RbOut.IsEnabled = false;
                    RbIn.Opacity = 0.5;
                    RbIn.IsEnabled = false;
                }
                else
                {
                    RbIn.Opacity = 0.5;
                    RbIn.IsEnabled = false;
                    RbOut.Opacity = 0.5;
                    RbOut.IsEnabled = false;
                }

            }
            else
            {
                SPScanner.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable datatable = SQL.ReturnDT(@"SELECT TOP(1)
Batchs.Name
, Batchs.Note
, Devices.Name
, LastOperationHistory.LastCount
, LastOperationHistory.Note
, Operations.Number
, Operations.Name
FROM Batchs
LEFT JOIN MSLs ON Batchs.IdMSL = MSLs.IdMSL
LEFT JOIN Devices ON MSLs.IdDevice = Devices.IdDevice
--ID последней записи
LEFT JOIN(
    SELECT OperationHistory.IdBatch
    , MAX(OperationHistory.IdRecording) as LastRecordingID

    FROM OperationHistory

    GROUP BY OperationHistory.IdBatch
) AS LastRecording ON Batchs.IdBatch = LastRecording.IdBatch
--Информация по последней записи
LEFT JOIN OperationHistory AS LastOperationHistory ON LastRecording.LastRecordingID = LastOperationHistory.IdRecording
--Информация по последней операции
LEFT JOIN Operations AS LastOperations ON LastOperationHistory.IdOperation = LastOperations.IdOperation
--Ищем следующую операцию
LEFT JOIN Operations ON Operations.Number > LastOperations.Number

WHERE Batchs.Barcode = " + TBCode.Text + "" +
                     "ORDER BY Operations.Number", App.configuration.SQLConnectionString, out string ex);

                TbTypeDevice.Text = datatable.Rows[0].ItemArray[2].ToString().Trim();
                TbNumberBatch.Text = datatable.Rows[0].ItemArray[0].ToString().Trim();
                TbNumberOperation.Text = datatable.Rows[0].ItemArray[5].ToString().Trim();
                TbNote.Text = datatable.Rows[0].ItemArray[6].ToString().Trim();
                TbCount.Text = datatable.Rows[0].ItemArray[3].ToString().Trim();
                TbNoteCount.Text = datatable.Rows[0].ItemArray[4].ToString().Trim();
                TbNoteBatch.Text = datatable.Rows[0].ItemArray[1].ToString().Trim();
            }

            catch (Exception)
            {

                throw;
            }
        }
            

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetUC();
        }

        void ResetUC()
        {
            RbIn.IsChecked = false;
            RbOut.IsChecked = false;
            RbIn.Opacity = 1;
            RbOut.Opacity = 1;
            RbIn.IsEnabled = true;
            RbOut.IsEnabled = true;
            TBCode.Text = null;
            TbTypeDevice.Text = null;
            TbNumberOperation.Text = null;
            TbNumberBatch.Text = null;
            TbNoteCount.Text = null;
            TbNote.Text = null;
            TbCount.Text = null;
            SPScanner.Visibility = Visibility.Hidden;
            GridInfo.Visibility = Visibility.Hidden;
            btn_Confirm.Visibility = Visibility.Hidden;
            btn_Reset.Visibility = Visibility.Hidden;
        }

        private void TBCode_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            {
               
            }
        }
    }
}
