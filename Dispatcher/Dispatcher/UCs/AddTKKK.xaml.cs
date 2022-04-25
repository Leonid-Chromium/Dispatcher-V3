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
	/// Логика взаимодействия для AddTKKK.xaml
	/// </summary>
	public partial class AddTKKK : UserControl
	{
		DataTable dataTable1 = new DataTable();
		DataTable dataTable2 = new DataTable();

		public AddTKKK()
		{
			InitializeComponent();
		}

		private void UpdateInfo()
		{
			dataTable1 = SQLLib.SQL.ReturnDT("SELECT Districts.IdDistrict, TRIM(Districts.Name) FROM Districts", App.configuration.SQLConnectionString, out string ex1);
			dataTable2 = SQLLib.SQL.ReturnDT("SELECT OperationType.idType, TRIM(OperationType.Name) From OperationType", App.configuration.SQLConnectionString, out string ex2);

			NameTB.Text = string.Empty;
			LarsTB.Text = string.Empty;
			PathTB.Text = string.Empty;
			DistrictCB.Text = null;
			OperationTypeCB.Text = null;
			DistrictCB.SelectedItem = null;
			OperationTypeCB.SelectedItem = null;


			DistrictCB.Items.Clear();
			for (int i = 1; i <= dataTable1.Rows.Count; i++)
			{
				ComboBoxItem comboBoxItem = new ComboBoxItem();
				comboBoxItem.Tag = dataTable1.Rows[i - 1].ItemArray[0];
				comboBoxItem.Content = dataTable1.Rows[i - 1].ItemArray[1];
				DistrictCB.Items.Add(comboBoxItem);
			}

			OperationTypeCB.Items.Clear();
			for (int i = 1; i <= dataTable2.Rows.Count; i++)
			{
				ComboBoxItem comboBoxItem = new ComboBoxItem();
				comboBoxItem.Tag = dataTable2.Rows[i - 1].ItemArray[0];
				comboBoxItem.Content = dataTable2.Rows[i - 1].ItemArray[1];
				OperationTypeCB.Items.Add(comboBoxItem);
			}
		}

		private void DistrictCB_DropDownClosed(object sender, EventArgs e)
		{

		}

		private void OperationTypeCB_DropDownClosed(object sender, EventArgs e)
		{

		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateInfo();
		}

		private void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			UpdateInfo();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			string nameStr = NameTB.Text.Trim();
			string codeStr = CodeTB.Text.Trim();
			string larsStr = LarsTB.Text.Trim();
			string pathStr = PathTB.Text.Trim();
			string districtStr = "";
			if (DistrictCB.SelectedItem != null)
				districtStr = ((ComboBoxItem)DistrictCB.SelectedItem).Tag.ToString();
			string operTypeStr = "";
			if (OperationTypeCB.SelectedItem != null)
				operTypeStr = ((ComboBoxItem)OperationTypeCB.SelectedItem).Tag.ToString();

			Trace.WriteLine("|" + nameStr + "|" + String.IsNullOrEmpty(nameStr));
			Trace.WriteLine("|" + codeStr + "|" + String.IsNullOrEmpty(codeStr));
			Trace.WriteLine("|" + larsStr + "|" + String.IsNullOrEmpty(larsStr));
			Trace.WriteLine("|" + pathStr + "|" + String.IsNullOrEmpty(pathStr));
			Trace.WriteLine("|" + districtStr + "|" + String.IsNullOrEmpty(districtStr));
			Trace.WriteLine("|" + operTypeStr + "|" + String.IsNullOrEmpty(operTypeStr));
			Trace.WriteLine(String.IsNullOrEmpty(nameStr) && String.IsNullOrEmpty(codeStr) && String.IsNullOrEmpty(larsStr) && String.IsNullOrEmpty(pathStr) && String.IsNullOrEmpty(districtStr) && String.IsNullOrEmpty(operTypeStr));

			if (String.IsNullOrEmpty(nameStr) || String.IsNullOrEmpty(codeStr) || String.IsNullOrEmpty(districtStr) || String.IsNullOrEmpty(operTypeStr))
			{
				MessageBox.Show("Вы что то не заполнили");
			}
			else
			{
				string query = @"
						INSERT INTO TechnologicalMaps
						(
						NameTM
						,Code
						,Lars
						,Path
						,District
						,idOperationType)
					VALUES
						('" + nameStr + "'" +
						",'" + codeStr + "'" +
						",'" + larsStr + "'" +
						",'" + pathStr + "'" +
						"," + districtStr +
						"," + operTypeStr +
						")";
				Trace.WriteLine(query);
				SQLLib.SQL.NoReturn(query, App.configuration.SQLConnectionString, out string ex);
				if (ex.Trim() != string.Empty)
					MessageBox.Show(ex);
			}
		}
	}
}
