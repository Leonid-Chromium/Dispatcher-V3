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

namespace Dispatcher.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        public bool PasswordCheck(int Level, string Password)
        {
            switch (Level)
            {
                case 0:
                    return true;

                case 1:
                    if (Password == "0000")
                        return true;
                    else
                        return false;

                default:
                    return false;
            }
        }
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите ли вы выбрать эту конфигурацию", "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);

            if (result == MessageBoxResult.Yes)
            {
               
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             DataTable RoleDT = SQL.ReturnDT("SELECT IdRole, RoleName FROM Roles", App.configuration.SQLConnectionString, out string ex);

            for (int i = 1; i <= RoleDT.Rows.Count; i++)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Tag = RoleDT.Rows[i - 1].ItemArray[0];
                comboBoxItem.Content = RoleDT.Rows[i - 1].ItemArray[1];
                RoleComboBox.Items.Add(comboBoxItem);
            }
        }
    }
}
