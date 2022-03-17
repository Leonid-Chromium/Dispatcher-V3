using Dispatcher.Class;
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
using SQLLib;
using DataLib;

namespace Dispatcher.UCs
{
	/// <summary>
	/// Логика взаимодействия для EquipmentEdit.xaml
	/// </summary>
	public partial class EquipmentEdit : UserControl
	{
		public int equipmentID { get; set; }
		public object parent { get; set; }

		public EquipmentEdit()
		{
			InitializeComponent();
		}

		public void UpdateInfo()
		{
			DataTable dataTable = SQL.ReturnDT(@"
/*
Скрипт для вывода последнего состяния станков в окно изменения статуса
*/
	
SELECT
Equipments.IdEquipment
,TRIM(Equipments.InventoryNumber) AS 'InventoryNumber'
,TRIM(Equipments.Name) AS 'EquipmentsName'
,TRIM(Districts.Name) AS 'DistrictsName'
,TRIM(Rooms.Number) AS 'RoomNumber'
,Equipments.Capacity
,Equipments.Lenght
,Equipments.Width
,Equipments.Height
,Equipments.Weight
,EquipmentStatus.IdStatus
,TRIM(EquipmentStatus.Name) AS 'StatusName'
FROM
Equipments
LEFT JOIN
(
	SELECT
	EquipmentStatusHistory.IdNode AS 'LastIdNode'
	,EquipmentStatusHistory.IdEquipment AS 'LastIdEquipment'
	,EquipmentStatusHistory.IdStatus AS 'LastIdStatus'
	,EquipmentStatusHistory.ChangeDateTime AS 'LastChangeDateTime'
	,EquipmentStatusHistory.IdEmployee AS 'LastIdEmployee'
	,EquipmentStatusHistory.Note AS 'LastNote'
	FROM
	EquipmentStatusHistory
	JOIN
	(
		SELECT
		max(EquipmentStatusHistory.IdNode) AS LastId
		,EquipmentStatusHistory.IdEquipment AS IdEquipment
		FROM EquipmentStatusHistory
		GROUP BY EquipmentStatusHistory.IdEquipment
	) AS LastEquipmentStatusHistory ON EquipmentStatusHistory.IdNode = LastEquipmentStatusHistory.LastId

) AS qwe ON LastIdEquipment = Equipments.IdEquipment
JOIN EquipmentStatus ON EquipmentStatus.IdStatus = qwe.LastIdStatus
JOIN Districts ON Equipments.District = Districts.IdDistrict
LEFT JOIN Rooms ON Rooms.IdRoom = Equipments.Room
WHERE Equipments.IdEquipment = " + equipmentID
, App.configuration.SQLConnectionString, out string ex1);
			if (String.IsNullOrWhiteSpace(ex1))
			{
				Trace.WriteLine("Ошибка в библиотеке SQL: " + ex1);
				Log.NewLog(200, "Ошибка в библиотеке SQL: " + ex1);
			}
			else
				DataClass.DTtoTrace(dataTable);

			//Таблица статусов
			DataTable statusDT = SQL.ReturnDT("", App.configuration.SQLConnectionString, out string ex2);
			if (String.IsNullOrWhiteSpace(ex2))
			{
				Trace.WriteLine("Ошибка в библиотеке SQL: " + ex2);
				Log.NewLog(200, "Ошибка в библиотеке SQL: " + ex2);
			}
			else
				DataClass.DTtoTrace(statusDT);

			//Заполнение комбо бокса статусов
			EquipStatus.Items.Clear();
			foreach (DataRow dataRow in statusDT.Rows)
			{
				ComboBoxItem item = new ComboBoxItem();
				item.Content = dataRow.ItemArray[0].ToString();
				EquipStatus.Items.Add(item);
			}


			EquipID.Text = dataTable.Rows[0].ItemArray[0].ToString().Trim();
			EquipInvent.Text = dataTable.Rows[0].ItemArray[1].ToString().Trim();
			EquipName.Text = dataTable.Rows[0].ItemArray[2].ToString().Trim();
			EquipDistrict.Text = dataTable.Rows[0].ItemArray[3].ToString().Trim();
			EquipRoom.Text = dataTable.Rows[0].ItemArray[4].ToString().Trim();
			EquipCapacity.Text = dataTable.Rows[0].ItemArray[5].ToString().Trim();
			EquipLenght.Text = dataTable.Rows[0].ItemArray[6].ToString().Trim();
			EquipWidth.Text = dataTable.Rows[0].ItemArray[7].ToString().Trim();
			EquipHeight.Text = dataTable.Rows[0].ItemArray[8].ToString().Trim();
			EquipWeight.Text = dataTable.Rows[0].ItemArray[9].ToString().Trim();
		}

		public void UpdateInfo(int id)
		{
			equipmentID = id;
			UpdateInfo();
		}

		private void SaveStatus_Click(object sender, RoutedEventArgs e)
		{
			SQL.ReturnDT(@"
Insert INTO EquipmentStatusHistory (IdEquipment, IdStatus, ChangeDateTime, IdEmployee, Note)
VALUES (" + EquipID.Text.Trim() + ", " + EquipStatus.Text.Trim() + ", CAST('" + DateTime.Now.ToString() + "' AS datetime), " + 1 + ", null)", App.configuration.SQLConnectionString, out string ex1);
			if (String.IsNullOrWhiteSpace(ex1))
			{
				Trace.WriteLine("Ошибка в библиотеке SQL: " + ex1);
				Log.NewLog(200, "Ошибка в библиотеке SQL: " + ex1);
			}
		}
	}
}
