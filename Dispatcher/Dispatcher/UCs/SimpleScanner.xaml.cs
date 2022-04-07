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

namespace Dispatcher.UCs
{
	/// <summary>
	/// Логика взаимодействия для SimpleScanner.xaml
	/// </summary>
	public partial class SimpleScanner : UserControl
	{
		int batchId { get; set; } = 0;
		int operationId { get; set; } = 0;
		int equipmentId { get; set; } = 0;
		int employeeId { get; set; } = 0;

		public SimpleScanner()
		{
			InitializeComponent();
			Reset();
		}

		private void Reset()
		{
			CancelButton.Visibility = Visibility.Collapsed;

			ScanInButton.IsEnabled = true;
			ScanOutButton.IsEnabled = true;
			ScanInButton.Opacity = 1;
			ScanOutButton.Opacity = 1;
			ScanInButton.IsChecked = false;
			ScanOutButton.IsChecked = false;

			BatchBarcodeTB.Text = null;
			BatchGrid.Visibility = Visibility.Collapsed;

			DeviceTypeTB.Text = null;
			BatchNameTB.Text = null;
			BatchPriorityTB.Text = null;
			BatchPriorityTB.Text = null;
			BatchNoteTB.Text = null;
			LastCountTB.Text = null;
			HistoryNoteTB.Text = null;
			EmployeeBarcodeTB.Text = null;
			EmployeeFIOTB.Text = null;
			EquipmentBarcodeTB.Text = null;
			EquipmentNameTB.Text = null;
			OperationKeyTB.Text = null;
			OperationNameTB.Text = null;
			OperationNoteTB.Text = null;
			InteroperativeTimeTB.Text = null;
			TypeOfProcessingTB.Text = null;
			TMNameTB.Text = null;
			ModeTB.Text = null;

			HistoryNoteLabel.Visibility = Visibility.Collapsed;
			HistoryNoteTB.Visibility = Visibility.Collapsed;

			InfoGrid.Visibility = Visibility.Collapsed;
		}

		private void CheckScanRadioButton()
		{
			ScanInButton.IsEnabled = false;
			ScanOutButton.IsEnabled = false;

			ScanInButton.Opacity = 0.5;
			ScanOutButton.Opacity = 0.5;

			CancelButton.Visibility = Visibility.Visible;

			BatchGrid.Visibility = Visibility.Visible;
		}

		private void UploadInfo()
		{
			DataTable infoDT = SQLLib.SQL.ReturnDT(
				@"
DECLARE @BARCODE NUMERIC (13, 0);
SET @BARCODE = 3456789123456
--переменная для id интересующей нас партии
DECLARE @THISBATCHID INT

--Да-да это отдельный скрипт для поиска IdBatch по штрихкоду
SELECT
@THISBATCHID = MAX(Batchs.IdBatch)
FROM Batchs
WHERE Batchs.Barcode = @BARCODE

PRINT concat('ID интересующей нас партии = ', @THISBATCHID)

SELECT
Batchs.IdBatch
--,Batchs.Barcode
,Batchs.Name
,Batchs.Priority
,Batchs.Note
,Device = CONCAT(TRIM(Devices.KeyDevice), ', ', TRIM(Devices.Name))
,LastOperationHistory.LastCount
,NextOperations.IdOperation
,NextOperations.OperationKey
,NextOperations.Name
,NextOperations.InteroperativeTime
,NextOperations.TypeOfProcessing
,TechnologicalMaps.NameTM
,TechnologicalMaps.Lars
,Routing.Mode
FROM ( SELECT * FROM Batchs WHERE Batchs.IdBatch = @THISBATCHID) AS Batchs
LEFT JOIN MSLs ON Batchs.IdMSL = MSLs.IdMSL
LEFT JOIN Devices ON MSLs.IdDevice = Devices.IdDevice
--ID последней записи
LEFT JOIN (
	SELECT
	OperationHistory.IdBatch
	,MAX(OperationHistory.IdRecording) as LastRecordingID
	FROM OperationHistory
	GROUP BY OperationHistory.IdBatch
) AS LastRecording ON Batchs.IdBatch = LastRecording.IdBatch
--Информация по последней записи
LEFT JOIN OperationHistory AS LastOperationHistory ON LastRecording.LastRecordingID = LastOperationHistory.IdRecording
--Информация по последней операции
LEFT JOIN Operations AS LastOperations ON LastOperationHistory.IdOperation = LastOperations.IdOperation
--Номер следующей операйции
LEFT JOIN (
	SELECT
	Batchs.IdBatch
	,MIN(NextOperations.Number) as 'NextOperationNumber'
	FROM Batchs
	LEFT JOIN MSLs ON Batchs.IdMSL = MSLs.IdMSL
	--ID последней записи
	LEFT JOIN (
		SELECT OperationHistory.IdBatch
		,MAX(OperationHistory.IdRecording) as LastRecordingID
		FROM OperationHistory
		GROUP BY OperationHistory.IdBatch
	) AS LastRecording ON Batchs.IdBatch = LastRecording.IdBatch
	--Информация по последней записи
	LEFT JOIN OperationHistory AS LastOperationHistory ON LastRecording.LastRecordingID = LastOperationHistory.IdRecording
	--Информация по последней операции
	LEFT JOIN Operations AS LastOperations ON LastOperationHistory.IdOperation = LastOperations.IdOperation
	--Ищем следующую операцию
	LEFT JOIN (
		SELECT
		*
		FROM Operations
		WHERE (Operations.ScanIn != 0) OR (Operations.ScanOut != 0)
	) AS NextOperations ON NextOperations.Number > LastOperations.Number AND NextOperations.IdMSL = LastOperations.IdMSL
	GROUP BY Batchs.IdBatch
) AS NextOperationsNumber ON Batchs.IdBatch = NextOperationsNumber.IdBatch
LEFT JOIN (
	SELECT * FROM Operations
) AS NextOperations ON NextOperationsNumber.NextOperationNumber = NextOperations.Number AND Batchs.IdMSL = NextOperations.IdMSL
LEFT JOIN Routing ON NextOperations.IdRouting = Routing.IdRouting
LEFT JOIN TechnologicalMaps ON Routing.IdTM = TechnologicalMaps.IdTM
", App.configuration.SQLConnectionString, out string ex);
			if (infoDT.Rows.Count > 0)
			{
				batchId = Convert.ToInt32(infoDT.Rows[0].ItemArray[0]);
				operationId = Convert.ToInt32(infoDT.Rows[0].ItemArray[6]);
				DeviceTypeTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[4]);
				BatchNameTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[1]);
				BatchPriorityTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[2]);
				BatchNoteTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[3]);
				LastCountTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[5]);
				OperationKeyTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[7]);
				OperationNameTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[8]);
				InteroperativeTimeTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[9]);
				TypeOfProcessingTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[10]);
				TMNameTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[11]);
				ModeTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[12]);
				InfoGrid.Visibility = Visibility.Visible;
			}
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Reset();
		}

		private void BatchBarcodeTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			string batchBarcode = BatchBarcodeTB.Text;
			batchBarcode =  batchBarcode.Trim();
			if (batchBarcode.Length == 13)
			{
				UploadInfo();
			}

		}

		private void EmployeeBarcodeTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			string employeeBarcode = EmployeeBarcodeTB.Text;
			employeeBarcode = employeeBarcode.Trim();
			if (employeeBarcode.Length == 13)
			{
				DataTable employeeDT = SQLLib.SQL.ReturnDT(@"
SELECT
Employees.IdEmployee
,CONCAT(TRIM(Employees.LastName), ' ', TRIM(Employees.FirstName), ' ', TRIM(Employees.MiddleName))
FROM Employees
WHERE Employees.Barcode = " + employeeBarcode,
				App.configuration.SQLConnectionString, out string ex);
				if (employeeDT.Rows.Count > 0)
				{
					employeeId = Convert.ToInt32(employeeDT.Rows[0].ItemArray[0]);
					EmployeeFIOTB.Text = Convert.ToString(employeeDT.Rows[0].ItemArray[1]);
				}
			}

				
		}

		private void EquipmentBarcodeTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			string equipmentBarcode = EquipmentBarcodeTB.Text;
			equipmentBarcode = equipmentBarcode.Trim();
			if (equipmentBarcode.Length == 13)
			{
				DataTable equipmentDT = SQLLib.SQL.ReturnDT(@"
SELECT
Equipments.IdEquipment
,Equipments.Name
FROM Equipments
WHERE Equipments.Barcode = " + equipmentBarcode,
				App.configuration.SQLConnectionString, out string ex);
				if(equipmentDT.Rows.Count > 0)
				{
					equipmentId = Convert.ToInt32(equipmentDT.Rows[0].ItemArray[0]);
					EquipmentNameTB.Text = Convert.ToString(equipmentDT.Rows[0].ItemArray[1]);
				}
			}

		}

		private void ConfirmationButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ScanInButton_Checked(object sender, RoutedEventArgs e)
		{
			CheckScanRadioButton();
		}

		private void ScanOutButton_Checked(object sender, RoutedEventArgs e)
		{
			CheckScanRadioButton();
		}
	}
}
