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
		DataTable GridTable = null;
        public SimpleBatchsViewer2()
        {
            InitializeComponent();
        }
		private void UpdateData()
		{
			DataTable batchsDataTable = new DataTable();
			batchsDataTable = SQLLib.SQL.ReturnDT(@"
SELECT
TRIM(Devices.KeyDevice) AS 'Ключ устройства'
,TRIM(Devices.Name) AS 'Название устройства'
,Batchs.IdBatch AS 'Идентификатор партии'
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

			GridTable = batchsDataTable;
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

			if (batchsDataTable.Rows.Count > 0)
			{
				GDBatchs.ItemsSource = batchsDataTable.DefaultView;				
			}			
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
			GridTable.DefaultView.RowFilter = string.Format("([Ключ устройства] LIKE '%{0}%' OR [Название устройства] LIKE '%{0}%' OR [Название партии] LIKE '%{0}%' OR [Примечание к последней партии] LIKE '%{0}%' OR [Название последней операции] LIKE '%{0}%') AND [Ключ устройства] LIKE '%{1}%'", (string)TbSearch.Text, ((ComboBoxItem)CmbFilter.SelectedItem).Tag);
		}
	}
}
