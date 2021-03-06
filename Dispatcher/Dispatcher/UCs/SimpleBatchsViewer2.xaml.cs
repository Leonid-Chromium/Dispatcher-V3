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
	/// Логика взаимодействия для SimpleBatchsViewer2.xaml
	/// </summary>
	public partial class SimpleBatchsViewer2 : UserControl
	{
		DataTable batchViewTable = null;
		DataTable historyBatchTable = null;
		public static int idBatch;
		public SimpleBatchsViewer2()
		{
			InitializeComponent();
		}
		
		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateData();

		}

		private void CmbFilter_DropDownClosed(object sender, EventArgs e)
		{
			RowFilter();
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			RowFilter();
		}

		private void RowFilter()
		{
			batchViewTable.DefaultView.RowFilter = string.Format("([Ключ устройства] LIKE '%{0}%' OR [Название устройства] LIKE '%{0}%' OR [Название партии] LIKE '%{0}%' OR [Примечание к последней партии] LIKE '%{0}%' OR [Название последней операции] LIKE '%{0}%') AND [Ключ устройства] LIKE '%{1}%'", (string)TbSearch.Text, ((ComboBoxItem)CmbFilter.SelectedItem).Tag);
		}

		private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			HistoryBatchSender(sender);
			HistoryBatch();
		}

		private void BtnBack_Click(object sender, RoutedEventArgs e)
		{
			UpdateData();
		}

		private void BtnUpdate_Click(object sender, RoutedEventArgs e)
		{
			if (historyBatchTable != null && GDBatchs.ItemsSource == historyBatchTable.DefaultView)
				HistoryBatch();
			else
				UpdateData();
		}

		private void UpdateData()
		{
			BtnBack.Visibility = Visibility.Hidden;
			batchViewTable = SQLLib.SQL.ReturnDT(@"
SELECT
Batchs.IdBatch AS 'Идентификатор партии'
,TRIM(Devices.Name) AS 'Название устройства'
,TRIM(Devices.KeyDevice) AS 'Ключ устройства'
,TRIM(Batchs.Name) AS 'Название партии'
,LastOperationHistory.LastCount AS 'Кол-во пластин'
,LastOperations.Number AS 'Номер последней операции'
,TRIM(LastOperations.Name) AS 'Название последней операции'
,Вход = CASE
WHEN LastOperations.ScanIn = 1
THEN '->○'
END
,Выход = CASE
WHEN LastOperations.ScanOut = 1
THEN '@->'
END
--,LastOperations.ScanIn AS 'Вход'
--,LastOperations.ScanOut AS 'Выход'
--,LastOperationHistory.StartDateTime AS 'Время на входе'
--,LastOperationHistory.EndDateTime AS 'Время на выходе'
,'Последнее время' = CASE
	WHEN LastOperationHistory.EndDateTime IS NOT NULL
	THEN LastOperationHistory.EndDateTime
	ELSE LastOperationHistory.StartDateTime
END

--,TRIM(Devices.KeyDevice) AS 'Код устройства'


--,Batchs.IdMSL

--Процент выполнения
------------------------
--,DoOperations.OperCount
--,TotalOperation.OperationCount
,(Cast(DoOperations.OperCount AS float)/CAST(TotalOperation.OperationCount AS float) * 100) AS 'Процент выполнения'

--,LastRecording.LastRecordingID
--,LastOperationHistory.IdOperation
--,LastOperations.IdOperation AS LastOperationID

,TRIM(LastOperationHistory.Note) AS 'Примечание к последней партии'

FROM Batchs
--Добавим МСЛы
LEFT JOIN MSLs ON Batchs.IdMSL = MSLs.IdMSL
--Добавим приборы
LEFT JOIN Devices ON MSLs.IdDevice = Devices.IdDevice

 
--Колличество отпераций всего
LEFT JOIN (
	SELECT
	Operations.IdMSL
	,COUNT(IdOperation) as OperationCount
	FROM Operations
	GROUP BY Operations.IdMSL
) AS TotalOperation ON Batchs.IdMSL = TotalOperation.IdMSL
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
--Наконец будем считать колличесво законченных операций
LEFT JOIN (
	SELECT
	tb.IdBatch
	,COUNT(tb.IdOperation) AS OperCount
	FROM (
		SELECT
		Batchs.IdBatch
		,Operations.IdOperation		
		FROM Batchs
		LEFT JOIN MSLs ON Batchs.IdMSL = MSLs.IdMSL
		RIGHT JOIN Operations ON MSLs.IdMSL = Operations.IdMSL

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
		--Выделяем только те операции что делаются до последней сделанной
		WHERE Operations.Number <= LastOperations.Number
	) as tb
	GROUP BY tb.IdBatch
) AS DoOperations ON Batchs.IdBatch = DoOperations.IdBatch
", App.configuration.SQLConnectionString, out string ex);

			DataTable NameDeviceTable = new DataTable();
			NameDeviceTable = SQLLib.SQL.ReturnDT("SELECT TRIM(Devices.KeyDevice), Devices.IdDevice FROM Devices", App.configuration.SQLConnectionString, out ex);

			CmbFilter.Items.Clear();

			ComboBoxItem comboBoxItem1 = new ComboBoxItem();
			comboBoxItem1.Tag = NameDeviceTable;
			comboBoxItem1.Content = "Все";
			comboBoxItem1.IsSelected = true;
			CmbFilter.Items.Add(comboBoxItem1);

			for (int i = 1; i <= NameDeviceTable.Rows.Count; i++)
			{
				ComboBoxItem comboBoxItem = new ComboBoxItem();
				comboBoxItem.Content = NameDeviceTable.Rows[i - 1].ItemArray[0];
				comboBoxItem.Tag = NameDeviceTable.Rows[i - 1].ItemArray[1];
				CmbFilter.Items.Add(comboBoxItem);
			}

			if (NameDeviceTable.Rows.Count > 0)
			{
				GDBatchs.ItemsSource = batchViewTable.DefaultView;
			}
		}

		private void HistoryBatchSender(object sender)
		{
			if (sender != null)
			{
				BtnBack.Visibility = Visibility.Visible;
				DataGridRow dgr = sender as DataGridRow;
				TextBlock tbl = GDBatchs.Columns[0].GetCellContent(dgr) as TextBlock;
				idBatch = Convert.ToInt32(tbl.Text);
			}
		}

		private void HistoryBatch()
		{
			historyBatchTable = SQLLib.SQL.ReturnDT(@"
SELECT
TRIM(Batchs.Name) AS 'Партия'
,Operations.ScanIn AS 'Вход'
,Operations.ScanOut AS 'Выход'
,TRIM(Operations.Name) AS 'Название операции'
,OperationHistory.LastCount AS 'Кол-во'
,TRIM(TechnologicalMaps.NameTM)
,TRIM(TechnologicalMaps.Lars)
,Routing.Mode AS 'Режим'
,Operations.InteroperativeTime AS 'Межоперационное время'
,Operations.TypeOfProcessing AS 'Тип постобработки'
,OperationHistory.StartDateTime AS 'Время входа'
,OperationHistory.EndDateTime AS 'Время выхода'
--,Employees.FirstName AS 'Имя сотрудника'
--,Employees.MiddleName AS 'Отчество сотрудника'
--,Employees.LastName AS 'Фамилия сотрудника'
,CONCAT(TRIM(Employees.FirstName), ' ', TRIM(Employees.MiddleName), ' ', TRIM(Employees.LastName)) AS 'ФИО сотрудника'
,TRIM(Equipments.Name) AS 'Название оборудования'
,TRIM(Rooms.Number) AS 'Комната'
,TRIM(Districts.Name) AS 'Участок'
,TRIM(Routing.Note) AS 'Примечание по МСЛ'
,TRIM(OperationHistory.Note) AS 'Коментарий к записи'
FROM Batchs
LEFT JOIN OperationHistory ON Batchs.IdBatch = OperationHistory.IdBatch
LEFT JOIN Operations ON OperationHistory.IdOperation = Operations.IdOperation
LEFT JOIN Employees ON OperationHistory.IdEmployee = Employees.IdEmployee
LEFT JOIN Equipments ON OperationHistory.IdEquipment = Equipments.IdEquipment
LEFT JOIN Rooms ON Equipments.Room = Rooms.IdRoom
LEFT JOIN Districts ON Rooms.IdDistrict = Districts.IdDistrict
LEFT JOIN Routing ON Operations.IdRouting = Routing.IdRouting
LEFT JOIN TechnologicalMaps ON Routing.IdTM = TechnologicalMaps.IdTM
WHERE Batchs.IdBatch = " + idBatch
, App.configuration.SQLConnectionString, out string ex);

			if (historyBatchTable.Rows.Count > 0)
			{
				GDBatchs.ItemsSource = historyBatchTable.DefaultView;
			}
		}
	}
}