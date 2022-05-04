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
	/// Логика взаимодействия для MonitorNextOperationOnDistrictUC.xaml
	/// </summary>
	public partial class MonitorNextOperationOnDistrictUC : UserControl
	{
		public MonitorNextOperationOnDistrictUC()
		{
			InitializeComponent();
		}

		public void Update()
		{
			DataTable dataTable = new DataTable();
			dataTable = SQLLib.SQL.ReturnDT(@"
/*
// Скрипт выведет оперделённое кол-во предстояящих операций на определённых участках
*/

DECLARE @ThisDistrict INT = " + Convert.ToInt32(App.configuration.district) + @"
DECLARE @Fork INT = 3

SELECT
LastOper.IdBatch
,NextOper.*
,Routing.Mode
,Routing.Name
,Routing.Note
,TechnologicalMaps.Code
,TechnologicalMaps.Lars
,TechnologicalMaps.NameTM
,TechnologicalMaps.Path
,Districts.Name
FROM (
	SELECT
	OperHisInfo.*
	FROM (
		SELECT
		OperationHistory.IdBatch
		,MaxRecordingId = MAX(OperationHistory.IdRecording)
		FROM OperationHistory
		GROUP BY OperationHistory.IdBatch
	) AS OperHis
	LEFT JOIN OperationHistory AS OperHisInfo ON OperHisInfo.IdRecording = OperHis.MaxRecordingId
) AS LastOper
--Operations for last operation of batchs
LEFT JOIN Operations AS LastOperInfo ON LastOper.IdOperation = LastOperInfo.IdOperation
--Opearatons for future operations
LEFT JOIN Operations AS NextOper ON (LastOperInfo.IdMSL = NextOper.IdMSL) AND (NextOper.Number > LastOperInfo.Number) AND (NextOper.Number <= (LastOperInfo.Number + @Fork))

LEFT JOIN Routing ON NextOper.IdRouting = Routing.IdRouting

LEFT JOIN TechnologicalMaps ON Routing.IdTM = TechnologicalMaps.IdTM

LEFT JOIN Districts ON TechnologicalMaps.District = Districts.IdDistrict

WHERE Districts.IdDistrict = @ThisDistrict AND NextOper.IdOperation IS NOT NULL
", App.configuration.SQLConnectionString, out string ex);
			if (String.IsNullOrWhiteSpace(ex))
			{
				MonitorDG.ItemsSource = dataTable.DefaultView;
			}
			else
			{
				MessageBox.Show(ex);
			}
		}

		private void UpdateButton_Click(object sender, RoutedEventArgs e)
		{
			Update();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			Update();
		}
	}
}
