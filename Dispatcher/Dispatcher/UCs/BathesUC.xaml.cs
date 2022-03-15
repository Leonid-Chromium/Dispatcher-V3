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
using DataLib;
using SQLLib;

namespace Dispatcher.UCs
{
    /// <summary>
    /// Логика взаимодействия для BathesUC.xaml
    /// </summary>
    public partial class BathesUC : UserControl
    {
        public BathesUC()
        {
            InitializeComponent();
            

        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TBIDBathes.Text != null)
                {
                    DataTable dataTable = SQL.ReturnDT("SELECT TRIM(Name) FROM Batchs WHERE (IdBatch = " + TBIDBathes.Text + ")", App.configuration.SQLConnectionString, out string ex);
                    TBNameBatches.Text = dataTable.Rows[0].ItemArray[0].ToString();

                    dataTable = SQL.ReturnDT(@"SELECT CONCAT(TRIM(FirstName), ' ', TRIM(MiddleName), ' ', TRIM(LastName)) 
                            AS 'Что-то на японском' FROM Employees WHERE(IdEmployee = " + TBIdEmployes.Text + ")", 
                            App.configuration.SQLConnectionString, out ex);
                    TBFullNameEmployees.Text = dataTable.Rows[0].ItemArray[0].ToString();

                    dataTable = SQL.ReturnDT(@"SELECT OperationHistory.LastCount FROM OperationHistory
                        Right join (SELECT OperationHistory.IdBatch,MAX(OperationHistory.IdRecording) AS 'LastIdRecording'
                        FROM OperationHistory  Group by IdBatch) AS LastRecording ON LastRecording.LastIdRecording = OperationHistory.IdRecording
                        WHERE OperationHistory.IdBatch = " + TBIDBathes.Text, App.configuration.SQLConnectionString, out ex);

                    TBLastCount.Text = dataTable.Rows[0].ItemArray[0].ToString().Trim();

                    dataTable = SQL.ReturnDT(@"SELECT Equipments.IdEquipment,Equipments.InventoryNumber,Equipments.Name From Equipments 
                        RIGHT JOIN (SELECT OperationHistory.IdBatch ,OperationHistory.LastCount ,OperationHistory.IdEquipment 
                        FROM OperationHistory Right join (SELECT OperationHistory.IdBatch,MAX(OperationHistory.IdRecording) AS 'LastIdRecording'
		                FROM OperationHistory Group by IdBatch) AS LastRecording ON LastRecording.LastIdRecording = OperationHistory.IdRecording) 
                        AS LastOperationInfo on Equipments.IdEquipment = LastOperationInfo.IdEquipment Where LastOperationInfo.IdBatch = " + TBIDBathes.Text, App.configuration.SQLConnectionString, out ex);

                    TBIDEquipment.Text = dataTable.Rows[0].ItemArray[0].ToString().Trim();
                    TBNumberEquipment.Text = dataTable.Rows[0].ItemArray[1].ToString().Trim();
                    TBNameEquipment.Text = dataTable.Rows[0].ItemArray[2].ToString().Trim();

                    dataTable = SQL.ReturnDT(@"SELECT maxNum = CASE WHEN MAX(Operations.Number) = null THEN 0 
                        ELSE MAX(Operations.Number) end,OperationHistory.IdBatch
                        FROM OperationHistory Left JOIN Operations on Operations.IdOperation = OperationHistory.IdOperation 
                        WHERE OperationHistory.IdBatch = " + TBIDBathes.Text + " Group by IdBatch", App.configuration.SQLConnectionString, out ex);
                    
                    TBNumberOperation.Text = dataTable.Rows[0].ItemArray[0].ToString().Trim();


                    dataTable = SQL.ReturnDT(@"SELECT Operations.Name FROM Operations RIGHT JOIN 
                        (SELECT maxNum = CASE WHEN MAX(Operations.Number) = null THEN 0 ELSE MAX(Operations.Number) end,OperationHistory.IdBatch
                        FROM OperationHistory Left JOIN Operations on Operations.IdOperation = OperationHistory.IdOperation 
                        WHERE OperationHistory.IdBatch = " + TBIDBathes.Text + " Group by IdBatch ) AS LastNumberOperation on (LastNumberOperation.maxNum = Operations.Number)", App.configuration.SQLConnectionString, out ex);

                    TBNameOperation.Text = dataTable.Rows[0].ItemArray[0].ToString().Trim();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(Convert.ToString(ex));
            }
            
        }
    }
}
