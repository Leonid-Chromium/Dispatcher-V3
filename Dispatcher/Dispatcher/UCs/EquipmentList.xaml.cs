using System;
using System.Collections.Generic;
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
using System.Data;
using System.Diagnostics;
using Dispatcher.Class;

namespace Dispatcher.UCs
{
	/// <summary>
	/// Логика взаимодействия для EquipmentList.xaml
	/// </summary>
	public partial class EquipmentList : UserControl
	{
		public EquipmentUC parent { get; set; }

		public DataTable EquipmentDT { get; set; } = new DataTable("Equipment data table");
		public DataTable DistrictDT { get; set; } = new DataTable("District data table");
		public DataTable StatusDT { get; set; } = new DataTable("Status data table");

		public EquipmentList()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Обновление данных
		/// </summary>
		/// <returns></returns>
		public int UpdateDate()
		{
			try
			{
				EquipmentDT = SQL.ReturnDT(@"
/*
Скрипт для вывода последнего состяния станков
*/
	
SELECT
Equipments.IdEquipment
,TRIM(Equipments.InventoryNumber) AS 'InventoryNumber'
,TRIM(Equipments.Name) AS 'EquipmentsName'
,TRIM(Districts.Name) AS 'DistrictsName'
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
", App.configuration.SQLConnectionString, out string ex1);
				if (!String.IsNullOrWhiteSpace(ex1))
				{
					App.logger.NewLog(400, "Ошибка в EquipmentList.UpdateDate при обращении в БД " + ex1);
				}

				DistrictDT = SQL.ReturnDT(@"
SELECT Name FROM Districts
", App.configuration.SQLConnectionString, out string ex2);
				if (!String.IsNullOrWhiteSpace(ex2))
				{
					App.logger.NewLog(401, "Ошибка в EquipmentList.UpdateDate при обращении в БД " + ex2);
				}

				StatusDT = SQL.ReturnDT(@"
SELECT Name FROM EquipmentStatus
", App.configuration.SQLConnectionString, out string ex3);
				if (!String.IsNullOrWhiteSpace(ex3))
				{
					App.logger.NewLog(402, "Ошибка в EquipmentList.UpdateDate при обращении в БД " + ex3);
				}

				if (String.IsNullOrWhiteSpace(ex1) && String.IsNullOrWhiteSpace(ex2) && String.IsNullOrWhiteSpace(ex3))
				{
					App.logger.NewLog(100, "Данные используемые в EquipmentList успешно загруженны");
				}

				return 0;
			}
			catch (Exception ex)
			{
				App.logger.NewLog(403, "Ошибка в EquipmentList.UpdateDate " + ex.Message);
				return 1;
			}
		}

		/// <summary>
		/// Обновление полей в шапке UC
		/// </summary>
		public void UpdateHeader()
		{
			try
			{
				FilterDistrict.Items.Clear();

				ComboBoxItem item1 = new ComboBoxItem();
				item1.Content = "Нет";
				FilterDistrict.Items.Add(item1);

				foreach (DataRow dataRow in DistrictDT.Rows)
				{
					ComboBoxItem item = new ComboBoxItem();
					item.Content = dataRow.ItemArray[0].ToString();
					FilterDistrict.Items.Add(item);
				}

				FilterStatus.Items.Clear();

				ComboBoxItem item2 = new ComboBoxItem();
				item2.Content = "Нет";
				FilterStatus.Items.Add(item2);

				foreach (DataRow dataRow in StatusDT.Rows)
				{
					ComboBoxItem item = new ComboBoxItem();
					item.Content = dataRow.ItemArray[0].ToString();
					FilterStatus.Items.Add(item);
				}
			}
			catch (Exception ex)
			{
				App.logger.NewLog(404, "Ошибка в EquipmentList.UpdateHeader " + ex.Message);
			}
		}

		/// <summary>
		/// Наполнение UC карточками оборудования
		/// </summary>
		/// <returns></returns>
		public int UpdateCards()
		{
			try
			{
				DataTable EquipmentDTV = FilterAndSort(EquipmentDT);
				ListSP.Children.Clear();
				foreach (DataRow row in EquipmentDTV.Rows)
				{
					EquipmentCard card = new EquipmentCard();

					if (row.ItemArray[0] == DBNull.Value)
						App.logger.NewLog(404, "Нет ID оборудования ");
					else
						card.ID = Convert.ToInt32(row.ItemArray[0]);

					if (row.ItemArray[2] == DBNull.Value)
						App.logger.NewLog(405, String.Format("Нет название оборудования " + Convert.ToInt32(row.ItemArray[0])));
					else
						card.name = row.ItemArray[2].ToString();

					if (row.ItemArray[3] == DBNull.Value)
						App.logger.NewLog(406, String.Format("Нет участка оборудования " + Convert.ToInt32(row.ItemArray[0])));
					else
						card.district = row.ItemArray[3].ToString();

					if (row.ItemArray[5] == DBNull.Value)
					{
						card.status = "Нет статуса";
						App.logger.NewLog(407, String.Format("Нет статуса оборудования " + Convert.ToInt32(row.ItemArray[0])));
					}
					else
						card.status = row.ItemArray[5].ToString();

					if (row.ItemArray[4] == DBNull.Value)
						App.logger.NewLog(408, String.Format("Нет Id статуса оборудования " + Convert.ToInt32(row.ItemArray[0])));
					else
						card.statusId = Convert.ToInt32(row.ItemArray[4]);

					card.parent = (EquipmentList)this;
					Trace.WriteLine("Parent for " + Convert.ToInt32(row.ItemArray[0]) + " " + card.parent);

					card.Margin = new Thickness(10.0);
					ListSP.Children.Add(card);
					card.update();
				}

				App.logger.NewLog(101, "Заполнили список оборудования");
				return 0;
			}
			catch (Exception ex)
			{
				App.logger.NewLog(409, "Ошибка в EquipmentList.UpdateCards " + ex.Message);
				return 1;
			}
		}

		/// <summary>
		/// Использование фильтрации оборудования
		/// </summary>
		/// <param name="dataTable"></param>
		/// <returns></returns>
		public DataTable FilterAndSort(DataTable dataTable)
		{
			try
			{
				Trace.WriteLine("--------------------");
				dataTable.TableName = "Equipments data table";
				DataView dataView = new DataView(dataTable);
				dataView.Table = dataTable;

				string filterByDistrict = "";
				string filterByStatus = "";
				string filterSearch = "";

				string sortByName = ((ComboBoxItem)SortName.SelectedItem).Tag.ToString();
				if (sortByName != "None")
				{
					dataView.Sort = "EquipmentsName " + sortByName;
				}

				if (((ComboBoxItem)FilterDistrict.SelectedItem) != null)
					if (((ComboBoxItem)FilterDistrict.SelectedItem).Content.ToString().Trim() != ((ComboBoxItem)FilterDistrict.Items[0]).Content.ToString().Trim())
					{
						filterByDistrict = "DistrictsName = '" + ((ComboBoxItem)FilterDistrict.SelectedItem).Content.ToString().Trim() + "'";
						Trace.WriteLine("filterByDistrict | " + filterByDistrict);
					}

				if (((ComboBoxItem)FilterStatus.SelectedItem) != null)
					if (((ComboBoxItem)FilterStatus.SelectedItem).Content.ToString().Trim() != ((ComboBoxItem)FilterStatus.Items[0]).Content.ToString().Trim())
					{
						filterByStatus = "StatusName = '" + ((ComboBoxItem)FilterStatus.SelectedItem).Content.ToString().Trim() + "'";
						Trace.WriteLine("filterByStatus   | " + filterByStatus);
					}

				string search = SearchTB.Text.Trim();
				if (search != String.Empty)
				{
					filterSearch = "(InventoryNumber LIKE '%" + search + "%' OR EquipmentsName LIKE '%" + search + "%' OR DistrictsName LIKE '%" + search + "%' OR StatusName LIKE '%" + search + "%')";
					Trace.WriteLine("FilterSearch     | " + filterSearch);
				}

				//отдельное применение запросов DataView
				string[] filterArr = { filterByDistrict, filterByStatus, filterSearch };
				string resultFilter = "";
				for (int i = 0; i < filterArr.Length; i++)
				{
					if (!resultFilter.EndsWith(" AND ") && filterArr[i] != "")
						if (!String.IsNullOrWhiteSpace(resultFilter))
							resultFilter = resultFilter + " AND ";
					resultFilter = resultFilter + filterArr[i];
				}

				Trace.WriteLine("                 |\nResult           | " + resultFilter);
				dataView.RowFilter = resultFilter;

				return dataView.ToTable();
			}
			catch (Exception ex)
			{
				App.logger.NewLog(410, "Ошибка в EquipmentList.FilterAndSort " + ex.Message);
				Trace.WriteLine(ex.Message);
				return dataTable;
			}
		}

		private void SortName_DropDownClosed(object sender, EventArgs e)
		{
			UpdateCards();
		}

		private void FilterDistrict_DropDownClosed(object sender, EventArgs e)
		{
			UpdateCards();
		}

		private void FilterStatus_DropDownClosed(object sender, EventArgs e)
		{
			UpdateCards();
		}

		private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			UpdateCards();
		}

		/// <summary>
		/// Нажатие на кнопку обновления
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RefreshButton_Click(object sender, RoutedEventArgs e)
		{
			UpdateDate();
			UpdateHeader();
			UpdateCards();
		}

		/// <summary>
		/// Срабатывает при загрузке UC
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateDate();
			UpdateHeader();
			UpdateCards();
		}
	}
}
