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
using System.Windows.Shapes;
using SQLLib;
using Dispatcher.Class;
using System.Diagnostics;

namespace Dispatcher.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public static int role { get; set; }
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            SetRole(Convert.ToInt32(RoleComboBox.Tag));
            Trace.WriteLine("Выбрана роль: " + role);
            string hash = Hashing.GetMD5Hash(PasswordBox.Password);
            Trace.WriteLine("Хэш: " + hash);
            DataTable dataTable = SQL.ReturnDT("SELECT RolePassword FROM Roles WHERE IdRole = '" + ((ComboBoxItem)RoleComboBox.SelectedItem).Tag.ToString() + "'", App.configuration.SQLConnectionString, out string ex);
            for (int i = 0; i < dataTable.Rows.Count; i++)
                if (dataTable.Rows[i].ItemArray[0].ToString() == hash)
                    MessageBox.Show("Salam"); //Тут реализовать вход
                else
                    MessageBox.Show("Неверный пароль"); //Тут обработка в случае неправильного ввода пароля
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите ли вы выбрать эту конфигурацию", "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);

            if (result == MessageBoxResult.Yes)
            {
                App.configuration = ConfigManage.GetConfiguration(ConfigComboBox.SelectedItem.ToString());
                ConfigManage.SetSavedConfiguration(ConfigComboBox.SelectedItem.ToString());
            }
            
            RoleLoaded();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigLoaded();
        }

        private void RoleLoaded()
        {
            try
            {
                RoleComboBox.Items.Clear();
                DataTable RoleDT = SQL.ReturnDT("SELECT IdRole, TRIM(RoleName) FROM Roles", App.configuration.SQLConnectionString, out string ex);

                for (int i = 1; i <= RoleDT.Rows.Count; i++)
                {
                    ComboBoxItem comboBoxItem = new ComboBoxItem();
                    comboBoxItem.Tag = RoleDT.Rows[i - 1].ItemArray[0];
                    comboBoxItem.Content = RoleDT.Rows[i - 1].ItemArray[1];
                    RoleComboBox.Items.Add(comboBoxItem);
                }
                
            }

            catch (Exception exc)
            {
                MessageBox.Show(Convert.ToString(exc));                
            }
            
        }

        private void ConfigLoaded()
        {
            try
            {
                ConfigComboBox.ItemsSource = ConfigManage.GetAllConfigurationName();
                ConfigComboBox.SelectedItem = App.configuration.name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
           
            RoleLoaded();
        }

        public void SetRole(int newRole)
        {
            role = newRole;
        }
    }
}
