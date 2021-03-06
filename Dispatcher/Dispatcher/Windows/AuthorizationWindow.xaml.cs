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
            App.logger.NewLog(200, "Открыто окно авторизации");
        }

        /// <summary>
        /// Обработка нажатия кнопки входа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
			try
			{
                //Проверка что выбрана какая-нибудь роль
                if((ComboBoxItem)RoleComboBox.SelectedItem != null)
				{
                    //Хэширование пароля для сравнения с БД
                    string hash = Hashing.GetMD5Hash(PasswordBox.Password);
                    //получения хэш пароля для выбранной роли
                    DataTable dataTable = SQL.ReturnDT("SELECT RolePassword FROM Roles WHERE IdRole = '" + ((ComboBoxItem)RoleComboBox.SelectedItem).Tag.ToString() + "'", App.configuration.SQLConnectionString, out string ex);
                    DataLib.DataClass.DTtoTrace(dataTable);
                    //перебор полученных из БД хэшей
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                        if (dataTable.Rows[i].ItemArray[0].ToString() == hash)
                        {
                            string str = ((ComboBoxItem)RoleComboBox.SelectedItem).Tag.ToString();
                            SetRole(Convert.ToInt32(str));

                            App.roleStr = Convert.ToString(((ComboBoxItem)RoleComboBox.SelectedItem).Content.ToString());
                            App.role = Convert.ToInt32(((ComboBoxItem)RoleComboBox.SelectedItem).Tag.ToString());
                            App.logger.NewLog(200, "Выбранна роль " + App.roleStr);

                            App.OpenMainWindow();
                            App.logger.NewLog(200, "Закрываем окно авторизации");
                            this.Close();
                        }
                        else
						{
                            App.logger.NewLog(300, "Неудачная попытка входа для роли " + ((ComboBoxItem)RoleComboBox.SelectedItem).Tag.ToString());
                            MessageBox.Show("Неверный пароль"); //Тут обработка в случае неправильного ввода пароля
                        }
                }
				else
				{
                    MessageBox.Show("Выберите роль");
				}
			}
            catch(Exception ex)
			{
                App.logger.NewLog(400, "Ошибка в AuthorizationWindow.EnterButton_Click " + ex.Message);
                MessageBox.Show(ex.Message);
			}
        }

        /// <summary>
        /// Подтверждение изменения используемой конфигурации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите ли вы выбрать эту конфигурацию", "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);

            if (result == MessageBoxResult.Yes)
            {
                App.configuration = ConfigManage.GetConfiguration(ConfigComboBox.SelectedItem.ToString());
                ConfigManage.SetSavedConfiguration(ConfigComboBox.SelectedItem.ToString());
                ConfigManage.CheckLogPath(App.configuration.logPath);
                ConfigManage.MakeLogFile(App.configuration.logPath);
            }
            
            RoleLoaded();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigLoaded();
        }

        /// <summary>
        /// Обновляет список ролей из БД
        /// </summary>
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

        /// <summary>
        /// Выводит список доступнрых конфигураций и выбирает нынешнюю
        /// </summary>
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
        
        /// <summary>
        /// Вносит ID роли из БД в переменную окна 
        /// </summary>
        /// <param name="newRole"></param>
        //Я не знаю зачем это нужно
        //TODO Убери этот рудимент
        public void SetRole(int newRole)
        {
            role = newRole;
        }
    }
}
