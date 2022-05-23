using System;
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

		/// <summary>
		/// Приведение сканера в "нулевое" состояние
		/// </summary>
		private void Reset()
		{
			try
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

				//HistoryNoteLabel.Visibility = Visibility.Collapsed;
				//HistoryNoteTB.Visibility = Visibility.Collapsed;

				InfoGrid.Visibility = Visibility.Collapsed;
			}
			catch (Exception ex)
			{
				App.logger.NewLog(400, "Ошибка в SimpleScanner.Reset " + ex.Message);
			}
		}

		/// <summary>
		/// Обработка нажатий на кнопки вход/выход
		/// </summary>
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

		/// <summary>
		/// Получение информации из БД
		/// </summary>
		private void DownloadInfo()
		{
			try
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
,InteroperativeTime =
	CASE
		WHEN Operation.InteroperativeTime IS NOT NULL
			THEN Operation.InteroperativeTime
		ELSE LastITAndTOP.InteroperativeTime
	END
,TypeOfProcessing =
	CASE
		WHEN Operation.TypeOfProcessing IS NOT NULL
			THEN Operation.TypeOfProcessing
		ELSE LastITAndTOP.TypeOfProcessing
	END
,TKKKName = TRIM(TechnologicalMaps.NameTM)
,TKKKLARS = TRIM(TechnologicalMaps.Lars)
,TKKKMode = Routing.Mode
,FirstMain.NowOperationID
--,OperationsScanIn = Operation.ScanIn
--,OperationsScanOut = Operation.ScanOut
,NextScanIsOut = 
	CASE
		--Вход и выход и вход пустой
		WHEN FirstMain.LastOperationID = Operation.IdOperation AND Operation.ScanIn = 1 AND Operation.ScanOut = 1 AND FirstMain.LastOperationStart IS NULL
		THEN 1
		--Вход и выход и вход не пустой
		WHEN FirstMain.LastOperationID = Operation.IdOperation AND Operation.ScanIn = 1 AND Operation.ScanOut = 1 AND FirstMain.LastOperationStart IS NOT NULL
		THEN 2
		--Случаи для
		ELSE
			CASE
				WHEN Operation.ScanIn = 1
				THEN 1
				WHEN Operation.ScanOut = 1
				THEN 2
				ELSE 0
			END
	END
,TechnologicalMaps.District
,Districts.Name

FROM (
		SELECT
		*
		FROM Batchs
		WHERE Batchs.IdBatch = @THISBATCHID
	) AS Batchs

--Скрипт для получения 'главной' информации
LEFT JOIN(
	SELECT
	LastOperationHistory.IdOperation AS 'LastOperationID'
	,LastOperationHistory.IdRecording AS 'LastRecordingID'
	,LastOperationHistory.StartDateTime AS 'LastOperationStart'
	,LastOperationHistory.EndDateTime AS 'LastOperationEnd'
	--,LastOperations.ScanIn AS 'LastOperationScanIn'
	--,LastOperations.ScanOut AS 'LastOperationScanOut'

	,NextOperations.ScanIn AS 'NextOperationsScanIn'
	,NextOperations.ScanOut AS 'NextOperationsScanOut'
	
	,BatchId = Batch.IdBatch
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
--Подключаем участки
LEFT JOIN Districts ON TechnologicalMaps.District = Districts.IdDistrict
--Получение МОВ и ТО вопреки всему
LEFT JOIN (
	SELECT TOP 1
	LastOperation.IdBatch
	,Operations.IdOperation
	,Operations.IdMSL
	,Operations.Number
	,Operations.InteroperativeTime
	,Operations.TypeOfProcessing
	FROM (
		SELECT * FROM Operations WHERE Operations.IdMSL = @THISBATCHMSL
	) AS Operations
	LEFT JOIN (
		SELECT TOP 1
		Batchs.IdBatch
		,OperationHistory.IdRecording
		,OperationHistory.IdOperation
		,Operations.IdMSL
		,Operations.Number
		FROM Batchs
		LEFT JOIN OperationHistory ON Batchs.IdBatch = OperationHistory.IdBatch
		LEFT JOIN Operations On OperationHistory.IdOperation = Operations.IdOperation
		WHERE Batchs.IdBatch = @THISBATCHID
		ORDER By OperationHistory.IdRecording DESC
	) AS LastOperation ON Operations.IdMSL <= LastOperation.IdMSL
	WHERE Operations.Number <= LastOperation.Number AND Operations.InteroperativeTime IS NOT NULL
	ORDER BY Operations.Number DESC
) AS LastITAndTOP ON Batchs.IdBatch = LastITAndTOP.IdBatch";
				Trace.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
				Trace.WriteLine(query);
				Trace.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-");
				App.logger.NewLog(200, "Сканируется партия " + BatchBarcodeTB.Text.Trim());
				infoDT = SQLLib.SQL.ReturnDT(query, App.configuration.SQLConnectionString, out string ex);
				//обработка ошибок возникших при обращении к запросу
				if (!String.IsNullOrWhiteSpace(ex))
				{
					App.logger.NewLog(401, "Ошибка В SimpleScanner.DownloadInfo при обращенни к БД" + ex);
					Trace.WriteLine("Ошибки запроса\n" + ex);
					Trace.WriteLine(ex);
				}

				DataLib.DataClass.DTtoTrace(infoDT);

				try
				{
					if (infoDT.Rows.Count > 0)
					{
						if (Convert.ToInt32(infoDT.Rows[0].ItemArray[16]) != 0)
						{
							switch (Convert.ToInt32(infoDT.Rows[0].ItemArray[16]))
							{
								case 1:
									if (ScanInButton.IsChecked == true)
										FillData();
									else
										MessageBox.Show("Партия должна сканироваться на вход");
									break;

								case 2:
									if (ScanOutButton.IsChecked == true)
										FillData();
									else
										MessageBox.Show("Партия должна сканироваться на выход");
									break;
							}
						}
						else
						{
							MessageBox.Show("Партия закрыта");
							Reset();
						}
					}
					else
					{
						App.logger.NewLog(402, "Нет строк в выборке по запросу сканера для партии " + BatchBarcodeTB.Text.Trim());
						MessageBox.Show("Строк нет");
					}
				}
				catch (System.InvalidCastException)
				{
					App.logger.NewLog(403, "Нет строк в выборке по запросу сканера для партии " + BatchBarcodeTB.Text.Trim());
					MessageBox.Show("Выборка пуста. Возможно вы ошиблись с выбором входа/выхода");
					Reset();
				}
			}
			catch (Exception ex)
			{
				App.logger.NewLog(404, "Ошибка В SimpleScanner.DownloadInfo " + ex.Message);
			}
		}

		/// <summary>
		/// Заполнение полей UC используя ранее получинными данными
		/// </summary>
		private void FillData()
		{
			try
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

				App.logger.NewLog(100, "Заполнили сканер данными");

				InfoGrid.Visibility = Visibility.Visible;

				if (!App.UnknownUserMode)
					EmployeeBarcodeTB.Focus();
				else
				{
					App.logger.NewLog(300, "В сканере используется режим игнорирования проверки на подпись");
					ConfirmationButton.Focus();
				}

				//Используется для дебагинга
				/*
				Trace.WriteLine(App.UnknownDistrictMode);
				Trace.WriteLine(infoDT.Rows[0].ItemArray[17].ToString() != String.Empty);
				Trace.WriteLine((infoDT.Rows[0].ItemArray[17].ToString() != String.Empty) && (Convert.ToInt32(App.configuration.district) == Convert.ToInt32(infoDT.Rows[0].ItemArray[17])));
				Trace.WriteLine(App.UnknownDistrictMode || ((infoDT.Rows[0].ItemArray[17].ToString() != String.Empty) && (Convert.ToInt32(App.configuration.district) == Convert.ToInt32(infoDT.Rows[0].ItemArray[17]))));

				Trace.WriteLine(App.UnknownDistrictMode);
				Trace.WriteLine(infoDT.Rows[0].ItemArray[17].ToString() != String.Empty);
				Trace.WriteLine(Convert.ToInt32(App.configuration.district));
				Trace.WriteLine(Convert.ToInt32(infoDT.Rows[0].ItemArray[17]));
				*/

				//Проверка на участок при получении данных
				//TODO Сделай это по человечески
				if (App.UnknownDistrictMode || ((infoDT.Rows[0].ItemArray[17].ToString() != String.Empty) && (Convert.ToInt32(App.configuration.district) == Convert.ToInt32(infoDT.Rows[0].ItemArray[17]))))
				{

				}
				else
				{
					string message = "";
					if (infoDT.Rows[0].ItemArray[17].ToString() == String.Empty)
						message = "В МСЛ не указан участок. Обратитесь к администратору";
					else
						message = "Участок не подошёл.\nНужен участок " + Convert.ToString(infoDT.Rows[0].ItemArray[18]);
					MessageBox.Show(message);
				}
			}
			catch (Exception ex)
			{
				App.logger.NewLog(405, "SimpleScanner.FillData " + ex.Message);
			}
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Reset();
		}

		/// <summary>
		/// Автоматическая загрузка данных при записи баркода партии
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BatchBarcodeTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			string batchBarcode = BatchBarcodeTB.Text;
			batchBarcode =  batchBarcode.Trim();
			if (batchBarcode.Length == 13)
			{
				DownloadInfo();
			}

		}

		/// <summary>
		/// Автоматическая загрузка данных о сотруднике
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EmployeeBarcodeTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
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

						App.logger.NewLog(101, "Нашли сотрудника по баркоду " + EmployeeBarcodeTB.Text.Trim());

						ConfirmationButton.Focus();
					}
					else
					{
						App.logger.NewLog(301, "Сканер не нашёл сотрудника по баркоду " + EmployeeBarcodeTB.Text.Trim());
					}
				}
			}
			catch (Exception ex)
			{
				App.logger.NewLog(406, "SimpleScanner.EmployeeBarcodeTB_TextChanged " + ex.Message);
			}
			
		}

		/// <summary>
		/// Автоматическая загрузка данных об оборудовании
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EquipmentBarcodeTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
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
					if (equipmentDT.Rows.Count > 0)
					{
						equipmentId = Convert.ToInt32(equipmentDT.Rows[0].ItemArray[0]);
						EquipmentNameTB.Text = Convert.ToString(equipmentDT.Rows[0].ItemArray[1]);

						App.logger.NewLog(102, "Нашли оборудование по баркоду " + EquipmentBarcodeTB.Text.Trim());
					}
					else
					{
						App.logger.NewLog(302, "Сканер не нашёл оборудование по баркоду " + EquipmentBarcodeTB.Text.Trim());
					}
				}
			}
			catch (Exception ex)
			{
				App.logger.NewLog(407, "SimpleScanner.EquipmentBarcodeTB_TextChanged " + ex.Message);
			}
		}

		/// <summary>
		/// Нажатие на кнопку подтверждения
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ConfirmationButton_Click(object sender, RoutedEventArgs e)
		{
			if (LastCountTB.Text.Trim() != Convert.ToString(infoDT.Rows[0].ItemArray[4]) && String.IsNullOrEmpty(HistoryNoteTB.Text.Trim()))
			{
				App.logger.NewLog(303, "Попытка сканирования партии без пояснения к измению кол-ва пластин в партии " + BatchNameTB.Text.Trim());
				MessageBox.Show("Введите пояснение к изменению кол-ва пластин");
				HistoryNoteTB.Focus();
			}
			else
			{
				//Проверка на участок
				if (App.UnknownDistrictMode || (Convert.ToInt32(App.configuration.district) == Convert.ToInt32(infoDT.Rows[0].ItemArray[17])))
				{
					//Проверка на оператора
					if (!String.IsNullOrEmpty(EmployeeBarcodeTB.Text.Trim()) || App.UnknownUserMode)
					{
						string query = @"
						INSERT INTO OperationHistory
						(IdOperation
						,IdBatch
						,StartDateTime
						,EndDateTime
						,Satellite"
						+ ((employeeId != 0) ? ",IdEmployee" : "")
						+ ((equipmentId != 0) ? ",IdEquipment" : "") + @"
						,Note
						,LastCount)
						VALUES
						(" + operationId +
						"," + batchId +
						"," + ((ScanInButton.IsChecked == true) ? "CAST('" + DateTime.Now.ToString() + "' as datetime)" : "NULL") +
						"," + ((ScanOutButton.IsChecked == true) ? "CAST('" + DateTime.Now.ToString() + "' as datetime)" : "NULL") +
						"," + "NULL" +
						((employeeId != 0) ? ("," + employeeId) : "") +
						((equipmentId != 0) ? ("," + equipmentId) : "") +
						", '" + HistoryNoteTB.Text.Trim() + "'" +
						"," + LastCountTB.Text + ")";

						Trace.WriteLine(query);
						SQLLib.SQL.NoReturn(query, App.configuration.SQLConnectionString, out string ex);
						if (!String.IsNullOrEmpty(ex))
						{
							App.logger.NewLog(408, "Ошибка при отправке операции над " + BatchNameTB.Text.Trim() + ". Запрос: " + query + ". Ошибка: " + ex);
						}
						else
						{
							App.logger.NewLog(201, "Операция для партии " + BatchNameTB.Text.Trim() + " отправленна в БД");
							MessageBox.Show("Успех!");
							Reset();
						}
					}
					else
					{
						MessageBox.Show("Введите свой штрих-код");
					}
				}
				else
				{
					App.logger.NewLog(304, "Попытка сканирования партии " + BatchNameTB.Text.Trim() + "на неподходящем участке");
					MessageBox.Show("Не подходит участок");
					Reset();
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

		/// <summary>
		/// Обработка Изменения колличества пластин
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LastCountTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (LastCountTB.Text.Trim() != Convert.ToString(infoDT.Rows[0].ItemArray[4]).Trim())
			{
				Trace.WriteLine("LastCountTB.Text = " + LastCountTB.Text.Trim());
				Trace.WriteLine("infoDT.Rows[0].ItemArray[5] = " + Convert.ToString(infoDT.Rows[0].ItemArray[4]).Trim());
				//HistoryNoteLabel.Visibility = Visibility.Visible;
				//HistoryNoteTB.Visibility = Visibility.Visible;
			}
			else
			{
				//HistoryNoteTB.Visibility = Visibility.Collapsed;
				//HistoryNoteLabel.Visibility = Visibility.Collapsed;
			}
		}
	}
}
