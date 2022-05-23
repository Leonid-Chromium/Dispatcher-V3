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

		/// <summary>
		/// Обновляет список ролей из БД
		/// </summary>
		private void UpdateRoleCB()
		{
			try
			{
				RoleCB.Items.Clear();
				RoleDT = SQL.ReturnDT("SELECT IdRole, TRIM(RoleName), RolePassword FROM Roles", App.configuration.SQLConnectionString, out string ex);
				if (!String.IsNullOrWhiteSpace(ex))
				{
					Trace.WriteLine(ex);
					App.logger.NewLog(400, "Ошибка в RoleSUC.UpdateRoleCB в sql-запросе" + ex);
				}
				
				foreach (DataRow dataRow in RoleDT.Rows)
				{
					ComboBoxItem comboBoxItem = new ComboBoxItem();
					comboBoxItem.Tag = dataRow.ItemArray[0];
					comboBoxItem.Content = dataRow.ItemArray[1];
					RoleCB.Items.Add(comboBoxItem);
				}

				App.logger.NewLog(100, "Наполнили список ролей в RoleSUC");
			}
			catch (Exception ex)
			{
				App.logger.NewLog(401, "Ошибка в RoleSUC.UpdateRoleCB " + ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Чистит поля UC
		/// </summary>
		public void RoleClean()
		{
			try
			{
				RoleCB.SelectedItem = null;
				RoleIdTB.Text = String.Empty;
				RoleNameTB.Text = String.Empty;
				RolePasswordTB.Text = String.Empty;
				RoleHashTB.Text = String.Empty;
				App.logger.NewLog(101, "Очистили поля RoleSUC");
			}
			catch (Exception ex)
			{
				App.logger.NewLog(402, "Ошибка в RoleSUC.RoleClean " + ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Добавляет новую роль в БД
		/// </summary>
		private void AddRole()
		{
			try
			{
				if (!(String.IsNullOrWhiteSpace(RoleNameTB.Text) || String.IsNullOrWhiteSpace(RoleHashTB.Text)))
				{
					SQL.NoReturn("INSERT INTO Roles (RoleName, RolePassword) VALUES ('" + RoleNameTB.Text + "', '" + RoleHashTB.Text + "')", App.configuration.SQLConnectionString, out string ex);
					App.logger.NewLog(200, "Отправили роль " + RoleNameTB.Text + " в БД");
					UpdateRoleCB();
					RoleClean();
				}
				else
				{
					App.logger.NewLog(102, "Попытка добавить роль " + RoleNameTB.Text + " не заполнив поля");
					MessageBox.Show("Какие-то нужные строки не заполнены");
				}
			}
			catch (Exception ex)
			{
				App.logger.NewLog(403, "Ошибка в RoleSUC.AddRole " + ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			AddRole();
		}

		/// <summary>
		/// Обновляет роль в БД
		/// </summary>
		private void EditRole()
		{
			try
			{
				if (!String.IsNullOrWhiteSpace(RoleNameTB.Text) || String.IsNullOrWhiteSpace(RoleHashTB.Text) || String.IsNullOrWhiteSpace(RoleIdTB.Text))
				{
					SQL.NoReturn("UPDATE Roles SET RoleName = '" + RoleNameTB.Text + "', RolePassword = '" + RoleHashTB.Text + "' WHERE IdRole = " + Convert.ToInt32(RoleIdTB.Text), App.configuration.SQLConnectionString, out string ex);
					App.logger.NewLog(201, "Отправили изменения роли " + RoleNameTB.Text + " в БД");
					UpdateRoleCB();
					RoleClean();
				}
				else
				{
					App.logger.NewLog(103, "Попытка изменить роль " + RoleNameTB.Text + " не заполнив поля");
					MessageBox.Show("Какие-то нужные строки не заполнены");
				}
			}
			catch (Exception ex)
			{
				App.logger.NewLog(404, "Ошибка в RoleSUC.EditRole " + ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void EditButton_Click(object sender, RoutedEventArgs e)
		{
			EditRole();
		}

		/// <summary>
		/// Удаляет роль из БД
		/// </summary>
		private void DeleteRole()
		{
			try
			{
				if (!String.IsNullOrWhiteSpace(RoleIdTB.Text))
				{
					SQL.NoReturn("DELETE FROM Roles WHERE IdRole = " + Convert.ToInt32(RoleIdTB.Text), App.configuration.SQLConnectionString, out string ex);
					App.logger.NewLog(202, "Удалили роль " + RoleNameTB.Text + " из БД");
					UpdateRoleCB();
					RoleClean();
				}
				else
				{
					App.logger.NewLog(104, "Попытка удалить роль " + RoleNameTB.Text + " не заполнив поля");
					MessageBox.Show("Какие-то нужные строки не заполнены");
				}
			}
			catch (Exception ex)
			{
				App.logger.NewLog(405, "Ошибка в RoleSUC.DeleteRole " + ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			DeleteRole();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateRoleCB();
		}

		//Для динамического обновления хэша пароля
		private void RolePasswordTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			RoleHashTB.Text = Class.Hashing.GetMD5Hash(RolePasswordTB.Text);
		}

		/// <summary>
		/// Заполняет поля UC данными о роли из БД
		/// </summary>
		private void FillRole()
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

					App.logger.NewLog(105, "Заполнили даные о роли  " + RoleNameTB.Text);
				}
			}
			catch (Exception ex)
			{
				App.logger.NewLog(406, "Ошибка в RoleSUC.DeleteRole " + ex.Message);
				Trace.WriteLine(ex.Message);
			}
		}

		private void RoleCB_DropDownClosed(object sender, EventArgs e)
		{
			FillRole();
		}
	}
}