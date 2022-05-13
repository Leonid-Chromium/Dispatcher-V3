using SQLLib;
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
	/// Логика взаимодействия для RoleSUC.xaml
	/// </summary>
	public partial class RoleSUC : UserControl
	{
		public DataTable RoleDT { get; set; } = new DataTable();

		public RoleSUC()
		{
			InitializeComponent();
		}

		private void UpdateRoleCB()
		{
			try
			{
				RoleCB.Items.Clear();
				RoleDT = SQL.ReturnDT("SELECT IdRole, TRIM(RoleName), RolePassword FROM Roles", App.configuration.SQLConnectionString, out string ex);
				Trace.WriteLine(ex);
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

		public void RoleClean()
		{
			RoleCB.SelectedItem = null;
			RoleIdTB.Text = String.Empty;
			RoleNameTB.Text = String.Empty;
			RolePasswordTB.Text = String.Empty;
			RoleHashTB.Text = String.Empty;
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			if (!(String.IsNullOrWhiteSpace(RoleNameTB.Text) || String.IsNullOrWhiteSpace(RoleHashTB.Text)))
			{
				SQL.NoReturn("INSERT INTO Roles (RoleName, RolePassword) VALUES ('" + RoleNameTB.Text + "', '" + RoleHashTB.Text + "')", App.configuration.SQLConnectionString, out string ex);
				UpdateRoleCB();
				RoleClean();
			}
			else
			{
				MessageBox.Show("Какие-то нужные строки не заполнены");
			}
		}

		private void EditButton_Click(object sender, RoutedEventArgs e)
		{
			if(!(String.IsNullOrWhiteSpace(RoleNameTB.Text) || String.IsNullOrWhiteSpace(RoleHashTB.Text) || String.IsNullOrWhiteSpace(RoleIdTB.Text)))
			{
				SQL.NoReturn("UPDATE Roles SET RoleName = '" + RoleNameTB.Text + "', RolePassword = '" + RoleHashTB.Text + "' WHERE IdRole = " + Convert.ToInt32(RoleIdTB.Text), App.configuration.SQLConnectionString, out string ex);
				UpdateRoleCB();
				RoleClean();
			}
			else
			{
				MessageBox.Show("Какие-то нужные строки не заполнены");
			}
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			if (!(String.IsNullOrWhiteSpace(RoleIdTB.Text)))
			{
				SQL.NoReturn("DELETE FROM Roles WHERE IdRole = " + Convert.ToInt32(RoleIdTB.Text), App.configuration.SQLConnectionString, out string ex);
				UpdateRoleCB();
				RoleClean();
			}
			else
			{
				MessageBox.Show("Какие-то нужные строки не заполнены");
			}
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			Trace.WriteLine("UserControl_Loaded");
			UpdateRoleCB();
		}

		private void RolePasswordTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			RoleHashTB.Text = Class.Hashing.GetMD5Hash(RolePasswordTB.Text);
		}

		private void RoleCB_DropDownClosed(object sender, EventArgs e)
		{
			try
			{
				if (RoleCB.SelectedItem != null)
				{
					int idSelectRole = Convert.ToInt32(((ComboBoxItem)RoleCB.SelectedItem).Tag);

					foreach (DataRow dataRow in RoleDT.Rows)
					{
						if (Convert.ToInt32(dataRow.ItemArray[0]) == idSelectRole)
						{
							RoleIdTB.Text = dataRow.ItemArray[0].ToString();
							RoleNameTB.Text = dataRow.ItemArray[1].ToString();
							RolePasswordTB.Text = String.Empty;
							RoleHashTB.Text = dataRow.ItemArray[2].ToString();
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.Message);
			}
		}
	}
}