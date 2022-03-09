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
using System.Windows.Shapes;

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
            //ComboBoxItem ComboItem = (ComboBoxItem)RoleComboBox.SelectedItem;
            //string Role = Convert.ToString(ComboItem.Tag);
            //if (PasswordCheck(Convert.ToInt32(Role), Convert.ToString(PasswordBox.Password)))
            //{
            //    //OpenMainWindow();
            //}
            //else
            //{
            //    MessageBox.Show("Неправильный пароль");
            //}
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите ли вы выбрать эту конфигурацию", "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);

            if (result == MessageBoxResult.Yes)
            {
               
            }
        }
    }
}
