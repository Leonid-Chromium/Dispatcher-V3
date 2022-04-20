﻿using System;
using System.Collections.Generic;
using System.Data;
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
	/// Логика взаимодействия для SimpleScanner.xaml
	/// </summary>
	public partial class SimpleScanner : UserControl
	{
		int batchId { get; set; } = 0;
		int operationId { get; set; } = 0;
		int equipmentId { get; set; } = 0;
		int employeeId { get; set; } = 0;

		DataTable infoDT = new DataTable();

		public SimpleScanner()
		{
			InitializeComponent();
			Reset();
		}

		private void Reset()
		{
			batchId = 0;
			operationId = 0;
			equipmentId = 0;
			employeeId = 0;

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

			//Переводм фокус на место для скана штрихкода партии
			BatchBarcodeTB.Focus();
		}

		private void UploadInfo()
		{
			string query = @"
DECLARE @BARCODE NUMERIC (13, 0) = " + BatchBarcodeTB.Text.Trim() +
@"
DECLARE @THISBATCHID INT
DECLARE @THISBATCHMSL INT

PRINT CONCAT('Batch barcode = ', @BARCODE)

--Да-да это отдельный скрипт для поиска IdBatch по штрих-коду
SELECT
@THISBATCHID = MAX(Batchs.IdBatch)
FROM Batchs
WHERE Batchs.Barcode = @BARCODE

PRINT CONCAT('ID интересующей нас партии = ', @THISBATCHID)

--А это скрипт для вывода МСЛ по которому делается эта партия
SELECT
@THISBATCHMSL = MAX(Batchs.IdMSL)
FROM Batchs
WHERE Batchs.IdBatch = @THISBATCHID

SELECT
FirstMain.BatchId
,FirstMain.BatchName
,FirstMain.BatchsPriority
,FirstMain.BatchsNote
,FirstMain.LastCount
,Device = TRIM(CONCAT(TRIM(Devices.KeyDevice), ', ', TRIM(Devices.Name)))
,OperationId = Operation.IdOperation
,OperationKey = TRIM(Operation.OperationKey)
,OperationName = TRIM(Operation.Name)
,OperationNote = TRIM(Operation.OperationNote)
,InteroperativeTime = Operation.InteroperativeTime
,TypeOfProcessing = Operation.TypeOfProcessing
,TKKKName = TRIM(TechnologicalMaps.NameTM)
,TKKKLARS = TRIM(TechnologicalMaps.Lars)
,TKKKMode = Routing.Mode
,FirstMain.NowOperationID
--,OperationsScanIn = Operation.ScanIn
--,OperationsScanOut = Operation.ScanOut
,NextScanIsOut = 
	CASE
		WHEN FirstMain.LastOperationID = Operation.IdOperation AND Operation.ScanIn = 1 AND Operation.ScanOut = 1 AND FirstMain.LastOperationStart IS NOT NULL
		THEN 1
		ELSE 0
	END
FROM (
		SELECT
		*
		FROM Batchs
		WHERE Batchs.IdBatch = @THISBATCHID
	) AS Batchs

--Скрипт для получения 'главной' информации
LEFT JOIN(
	SELECT
	BatchId = Batch.IdBatch
	, BatchName = TRIM(Batch.Name)
	, BatchsPriority = Batch.Priority
	, BatchsNote = TRIM(Batch.Note)
	, NowOperationID =
		CASE
			--Проверка на начало партии
			WHEN LastRecording.LastRecordingID IS NULL
				THEN FirstOperation.IdOperation
			--Проверка на конец партии
			WHEN (
				SELECT
				MAX(Operations.Number)
				FROM Operations
				WHERE Operations.IdMSL = @THISBATCHMSL
				) < NextOperationsNumber.NextOperationNumber
				THEN 0
			--Проверка на вход и выход
			WHEN LastOperations.ScanIn = 1 AND LastOperations.ScanOut = 1 AND
				LastOperationHistory.StartDateTime IS NOT NULL AND LastOperationHistory.EndDateTime IS NULL
				THEN LastOperations.IdOperation
			--Остальное
			ELSE NextOperations.IdOperation
		END
	,LastCount =
		CASE
			--Проверка на начало партии
			WHEN LastRecording.LastRecordingID IS NULL
				THEN Batch.StartCount
			ELSE LastOperationHistory.LastCount
		END
	--,LastOperationHistory.LastCount

	--Выборка партии с подходящим ID партии
	FROM(
		SELECT
		*
		FROM Batchs
		WHERE Batchs.IdBatch = @THISBATCHID
	) AS Batch

	--Первая операция
	LEFT JOIN(
		SELECT
		Operations2.*
		FROM Operations AS Operations2
		RIGHT JOIN(
			SELECT
			Operations.IdMSL
			, MinOperationNumber = MIN(Operations.Number)
			FROM Operations
			GROUP BY Operations.IdMSL
		) AS Operations1 ON Operations1.IdMSL = Operations2.IdMSL AND MinOperationNumber = Operations2.Number
	) AS FirstOperation ON Batch.IdMSL = FirstOperation.IdMSL

	--ID последней записи
	LEFT JOIN(
		SELECT
		OperationHistory.IdBatch
		, MAX(OperationHistory.IdRecording) AS LastRecordingID
		FROM OperationHistory
		GROUP BY OperationHistory.IdBatch
	) AS LastRecording ON Batch.IdBatch = LastRecording.IdBatch
	--Информация по последней записи
	LEFT JOIN OperationHistory AS LastOperationHistory ON LastRecording.LastRecordingID = LastOperationHistory.IdRecording
	--Информация по последней операции
	LEFT JOIN Operations AS LastOperations ON LastOperationHistory.IdOperation = LastOperations.IdOperation

	--Номер следующей операции
	LEFT JOIN(
		SELECT
		Batchs.IdBatch
		, MIN(NextOperations.Number) AS 'NextOperationNumber'
		FROM Batchs
		LEFT JOIN MSLs ON Batchs.IdMSL = MSLs.IdMSL
		--ID последней записи
		LEFT JOIN(
			SELECT OperationHistory.IdBatch
			, MAX(OperationHistory.IdRecording) AS LastRecordingID
			FROM OperationHistory
			GROUP BY OperationHistory.IdBatch
		) AS LastRecording ON Batchs.IdBatch = LastRecording.IdBatch
		--Информация по последней записи
		LEFT JOIN OperationHistory AS LastOperationHistory ON LastRecording.LastRecordingID = LastOperationHistory.IdRecording
		--Информация по последней операции
		LEFT JOIN Operations AS LastOperations ON LastOperationHistory.IdOperation = LastOperations.IdOperation
		--Ищем следующую операцию
		LEFT JOIN(
			SELECT
			*
			FROM Operations
			WHERE(Operations.ScanIn != 0) OR(Operations.ScanOut != 0)
		) AS NextOperations ON NextOperations.Number > LastOperations.Number AND NextOperations.IdMSL = LastOperations.IdMSL
		GROUP BY Batchs.IdBatch
	) AS NextOperationsNumber ON Batch.IdBatch = NextOperationsNumber.IdBatch
	--Информация по следующей операции
	LEFT JOIN Operations AS NextOperations ON NextOperationsNumber.NextOperationNumber = NextOperations.Number AND Batch.IdMSL = NextOperations.IdMSL

	
) AS FirstMain ON Batchs.IdBatch = FirstMain.BatchId

--Подключаем МСЛ
LEFT JOIN MSLs ON Batchs.IdMSL = MSLs.IdMSL
--Подключаем Девайс
LEFT JOIN Devices ON MSLs.IdDevice = Devices.IdDevice
--Подключаем операции
LEFT JOIN Operations AS Operation ON FirstMain.NowOperationID = Operation.IdOperation

--Подключаем роутиги(да что это такое?)
LEFT JOIN Routing ON Operation.IdRouting = Routing.IdRouting
--Подключаем Т.К./ К.К.
LEFT JOIN TechnologicalMaps ON Routing.IdTM = TechnologicalMaps.IdTM

WHERE Operation." + ((ScanInButton.IsChecked == true) ? "ScanIn" : "ScanOut") + " = 1";
			Trace.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
			Trace.WriteLine(query);
			Trace.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-");
			infoDT = SQLLib.SQL.ReturnDT(query, App.configuration.SQLConnectionString, out string ex);
			DataLib.DataClass.DTtoTrace(infoDT);
			//if (infoDT.Rows.Count > 0)
			//{
				
			//}
			//else
			//	MessageBox.Show("Ничего нет");
			try
			{
				if((infoDT.Rows.Count > 0))
				{
					if()
					if (Convert.ToInt32(infoDT.Rows[0].ItemArray[15]) != 0)
					{
						batchId = Convert.ToInt32(infoDT.Rows[0].ItemArray[0]);
						operationId = Convert.ToInt32(infoDT.Rows[0].ItemArray[6]);
						DeviceTypeTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[5]);
						BatchNameTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[1]);
						BatchPriorityTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[2]);
						BatchNoteTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[3]);
						LastCountTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[4]);
						OperationKeyTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[7]);
						OperationNameTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[8]);
						OperationNoteTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[9]);
						InteroperativeTimeTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[10]);
						TypeOfProcessingTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[11]);
						TMNameTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[12]);
						ModeTB.Text = Convert.ToString(infoDT.Rows[0].ItemArray[14]);

						InfoGrid.Visibility = Visibility.Visible;

						EmployeeBarcodeTB.Focus();
					}
					else
					{
						MessageBox.Show("Партия закрыта");
						Reset();
					}
				}
				else
				{
					MessageBox.Show("Строк нет");
				}
			}
			catch (System.InvalidCastException)
			{
				MessageBox.Show("Выборка пуста. Возможно вы ошиблись с выбором входа/выхода");
				Reset();
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

					ConfirmationButton.Focus();
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
			if (LastCountTB.Text.Trim() != Convert.ToString(infoDT.Rows[0].ItemArray[4]) && String.IsNullOrEmpty(HistoryNoteTB.Text.Trim()))
			{
				MessageBox.Show("Введите пояснение к изменению кол-ва пластин");
				HistoryNoteTB.Focus();
			}
			else
			{
				if (!String.IsNullOrEmpty(EmployeeBarcodeTB.Text.Trim()))
				{
					string query = @"
INSERT INTO OperationHistory
(IdOperation
,IdBatch
,StartDateTime
,EndDateTime
,Satellite
,IdEmployee"
+ ((equipmentId != 0) ? ",IdEquipment" : "") + @"
,Note
,LastCount)
VALUES
(" + operationId +
"," + batchId +
"," + ((ScanInButton.IsChecked == true) ? "CAST('" + DateTime.Now.ToString() + "' as datetime)" : "NULL") +
"," + ((ScanOutButton.IsChecked == true) ? "CAST('" + DateTime.Now.ToString() + "' as datetime)" : "NULL") +
"," + "NULL" +
"," + employeeId +
((equipmentId != 0) ? ("," + equipmentId) : "") +
", '" + HistoryNoteTB.Text.Trim() + "'" +
"," + LastCountTB.Text + ")";
					Trace.WriteLine(query);
					SQLLib.SQL.NoReturn(query, App.configuration.SQLConnectionString, out string ex);
					if (!String.IsNullOrEmpty(ex))
						MessageBox.Show(ex);
					else
					{
						MessageBox.Show("Успех!");
						Reset();
					}
				}
				else
				{
					MessageBox.Show("Введите свой штрих-код");
				}
				
			}
		}

		private void ScanInButton_Checked(object sender, RoutedEventArgs e)
		{
			CheckScanRadioButton();
		}

		private void ScanOutButton_Checked(object sender, RoutedEventArgs e)
		{
			CheckScanRadioButton();
		}

		private void LastCountTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (LastCountTB.Text.Trim() != Convert.ToString(infoDT.Rows[0].ItemArray[4]).Trim())
			{
				Trace.WriteLine("LastCountTB.Text = " + LastCountTB.Text.Trim());
				Trace.WriteLine("infoDT.Rows[0].ItemArray[5] = " + Convert.ToString(infoDT.Rows[0].ItemArray[4]).Trim());
				HistoryNoteLabel.Visibility = Visibility.Visible;
				HistoryNoteTB.Visibility = Visibility.Visible;
			}
			else
			{
				HistoryNoteTB.Visibility = Visibility.Collapsed;
				HistoryNoteLabel.Visibility = Visibility.Collapsed;
			}
		}
	}
}
