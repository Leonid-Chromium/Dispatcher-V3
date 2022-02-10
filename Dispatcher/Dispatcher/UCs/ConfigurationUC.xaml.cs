using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ConfigurationUC.xaml
    /// </summary>
    public partial class ConfigurationUC : UserControl
    {
        public object parent { get; set; }

        public ConfigurationUC()
        {
            InitializeComponent();
            ConfigurationCB.ItemsSource = Class.ConfigManage.GetAllConfigurationName();
        }

        public int updateInfoAboutConfig()
        {
            try
            {
                SQLConnectionStringTB.Text = App.configuration.SQLConnectionString;
                serverIpTB.Text = App.configuration.serverIp;
                serverPortTB.Text = App.configuration.serverPort;
                netPathTB.Text = App.configuration.netPath;
                DistrictTB.Text = App.configuration.district;
                roomTB.Text = App.configuration.room;
                themePathTB.Text = App.configuration.themePath;
                themeTB.Text = App.configuration.theme;
                assetsPathTB.Text = App.configuration.assetsPath;
                logPathTB.Text = App.configuration.logPath;

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("parent: " + parent);
            Windows.TestWindow testWindow = (Windows.TestWindow)parent;
            Trace.WriteLine("parent`s parent: " + testWindow.parent);
            ConfigurationCB.ItemsSource = Class.ConfigManage.GetAllConfigurationName();
            ConfigurationCB.SelectedItem = App.configuration.name;
            updateInfoAboutConfig();
            
        }

        private void ConfigurationCB_DropDownClosed(object sender, EventArgs e)
        {
            Trace.WriteLine(ConfigurationCB.SelectedItem.ToString());
            App.configuration = Class.ConfigManage.GetConfiguration(ConfigurationCB.SelectedItem.ToString());
            updateInfoAboutConfig();
        }

        private void SQLConnectionStringTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Trace.WriteLine("Строка подключения изменена");
        }

        private void serverIpTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Trace.WriteLine("Ip сервера изменён");
        }

        private void serverPortTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Trace.WriteLine("Порт сервера изменён");
        }

        private void netPathTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Trace.WriteLine("Сетевая папка изменена");
        }

        private void DistrictTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Trace.WriteLine("Участок изменён");
        }

        private void roomTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Trace.WriteLine("Комната изменена");
        }

        private void themePathTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Trace.WriteLine("Папка с темами изменена");
        }

        private void themeTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Trace.WriteLine("Выбранная тема изменена");
        }

        private void assetsPathTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Trace.WriteLine("Папка асетов изменена");
        }

        private void logPathTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Trace.WriteLine("Папка логов изменена");
        }

        private void delConfButton_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("Конфигурацию собираются удалить");
            MessageBoxResult result = MessageBox.Show("Вы уверены что собираетесь удалить крнфигурацию.\nЭта операция необратима", "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
            Trace.WriteLine("result = " + result);
            if(result == MessageBoxResult.Yes)
            {
                //TODO Сделать удаление конфигурации
                Trace.WriteLine("Конфигурация удалена");
            }
            else
            {
                Trace.WriteLine("Отмена удаления");
            }
        }
    }
}
