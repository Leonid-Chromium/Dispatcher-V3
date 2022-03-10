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

using SQLLib;

namespace Dispatcher.UCs
{
	/// <summary>
	/// Логика взаимодействия для SuperUserUC.xaml
	/// </summary>
	public partial class SuperUserUC : UserControl
	{
		public SuperUserUC()
		{
			InitializeComponent();
		}

		private void UpdateRoleCB()
		{
			try
			{
				RoleCB.Items.Clear();
				DataTable RoleDT = SQL.ReturnDT("SELECT IdRole, TRIM(RoleName)", App.configuration.SQLConnectionString, out string ex);
				foreach (DataRow dataRow in RoleDT.Rows)
				{
					ComboBoxItem comboBoxItem = new ComboBoxItem();
					comboBoxItem.Tag = dataRow.ItemArray[0];
					comboBoxItem.Content = dataRow.ItemArray[1];
					RoleCB.Items.Add(comboBoxItem);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void EditButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateRoleCB();
		}
	}
}
