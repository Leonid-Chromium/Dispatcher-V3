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
using Dispatcher.Class;

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
            updateInfoAboutConfig();
        }

        public int updateInfoAboutConfig()
        {
            try
            {
                ConfigurationCB.ItemsSource = ConfigManage.GetAllConfigurationName();
                ConfigurationCB.SelectedItem = App.configuration.name;
                SQLConnectionStringTB.Text = App.configuration.SQLConnectionString;
                serverIpTB.Text = App.configuration.serverIp;
                serverPortTB.Text = App.configuration.serverPort;
                netPathTB.Text = App.configuration.netPath;
                districtTB.Text = App.configuration.district;
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
            updateInfoAboutConfig();
            
        }

        private void ConfigurationCB_DropDownOpened(object sender, EventArgs e)
        {
            updateInfoAboutConfig();
        }

        private void ConfigurationCB_DropDownClosed(object sender, EventArgs e)
        {
            //TODO Сделай защиту от пустого выбора
            Trace.WriteLine(ConfigurationCB.SelectedItem.ToString());
            App.configuration = ConfigManage.GetConfiguration(ConfigurationCB.SelectedItem.ToString());
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

        private void districtTB_TextChanged(object sender, TextChangedEventArgs e)
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

        private int DeleteConfiguration1()
        {
            try
            {
                Trace.WriteLine("Конфигурацию " + ConfigurationCB.SelectedItem.ToString() + " собираются удалить");

                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить крнфигурацию.\nЭта операция необратима", "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
                Trace.WriteLine("result = " + result);

                if (result == MessageBoxResult.Yes)
                {
                    //TODO Сделать удаление конфигурации
                    Trace.WriteLine("Конфигурация удалена");
                    Log.NewLog(001, "Конфигурацию " + ConfigurationCB.SelectedItem.ToString() + " удалили");
                }
                else
                {
                    Trace.WriteLine("Отмена удаления");
                }
                updateInfoAboutConfig();

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        private int DeleteConfiguration2()
        {
            try
            {
                Trace.WriteLine("Конфигурацию " + ConfigurationCB.SelectedItem.ToString() + " собираются удалить");

                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить крнфигурацию " + App.configuration.name + ".\nЭта операция необратима",
                    "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
                Trace.WriteLine("result = " + result);

                if (result == MessageBoxResult.Yes)
                {
                    //TODO Сделать удаление конфигурации
                    ConfigManage.DeleteConfiguration(App.configuration.name);
                    App.configuration = ConfigManage.GetConfiguration(ConfigManage.GetAllConfigurationName().First());
                    Trace.WriteLine("Конфигурация удалена");
                    Log.NewLog(101, "Конфигурацию " + ConfigurationCB.SelectedItem.ToString() + " удалили");
                }
                else
                {
                    Trace.WriteLine("Отмена удаления");
                }
                updateInfoAboutConfig();

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        private void delConfButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteConfiguration2();
        }

        private int SaveConfiguration1()
        {
            try
            {
                Trace.WriteLine("Готовимся изменять конфигурацию");

                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите изменить крнфигурацию.\nЭта операция необратима", "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
                Trace.WriteLine("result = " + result);

                if (result == MessageBoxResult.Yes)
                {
                    Configuration newConfiguration = new Configuration();
                    newConfiguration.name = ConfigurationCB.Text.ToString();
                    newConfiguration.themePath = themePathTB.Text;
                    newConfiguration.theme = themeTB.Text;
                    newConfiguration.assetsPath = assetsPathTB.Text;
                    newConfiguration.logPath = logPathTB.Text;
                    newConfiguration.SQLConnectionString = SQLConnectionStringTB.Text;
                    newConfiguration.serverIp = serverIpTB.Text;
                    newConfiguration.serverPort = serverPortTB.Text;
                    newConfiguration.netPath = netPathTB.Text;
                    newConfiguration.district = districtTB.Text;
                    newConfiguration.room = roomTB.Text;

                    // Проверка на неповторяемость имён конфигурации
                    // WARNING Очень криво
                    while (ConfigManage.HaveName(newConfiguration.name))
                        newConfiguration.name = String.Concat(newConfiguration.name, " Дублёр");

                    ConfigManage.ChangeConfiguration(App.configuration.name, newConfiguration); //Изменяем конфигурацию в файле конфигурации
                    App.configuration = ConfigManage.GetConfiguration(newConfiguration.name); //Выбираем её для использования
                    Log.NewLog(001, "Конфигурацию " + App.configuration.name + " изменили" + ((newConfiguration.name != App.configuration.name) ? (" на " + newConfiguration.name) : "")); // Это тернарный оператор. Я не хочу об этом говорить

                    updateInfoAboutConfig();
                }

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        private int SaveConfiguration2()
        {
            try
            {
                Trace.WriteLine("Готовимся изменять конфигурацию");

                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите изменить крнфигурацию.\nЭта операция необратима", "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
                Trace.WriteLine("result = " + result);

                if (result == MessageBoxResult.Yes)
                {
                    Configuration newConfiguration = new Configuration();
                    newConfiguration.name = ConfigurationCB.Text.ToString();
                    newConfiguration.themePath = themePathTB.Text;
                    newConfiguration.theme = themeTB.Text;
                    newConfiguration.assetsPath = assetsPathTB.Text;
                    newConfiguration.logPath = logPathTB.Text;
                    newConfiguration.SQLConnectionString = SQLConnectionStringTB.Text;
                    newConfiguration.serverIp = serverIpTB.Text;
                    newConfiguration.serverPort = serverPortTB.Text;
                    newConfiguration.netPath = netPathTB.Text;
                    newConfiguration.district = districtTB.Text;
                    newConfiguration.room = roomTB.Text;

                    // Проверка на неповторяемость имён конфигурации
                    // WARNING Очень криво и что то меняем
                    while (ConfigManage.HaveName(newConfiguration.name) && (newConfiguration.name != App.configuration.name))
                        newConfiguration.name = String.Concat(newConfiguration.name, " Дублёр");

                    Trace.WriteLine(App.configuration.name);
                    newConfiguration.TraceConfiguration();
                    ConfigManage.ChangeConfiguration(App.configuration.name, newConfiguration); //Изменяем конфигурацию в файле конфигурации
                    App.configuration = ConfigManage.GetConfiguration(newConfiguration.name); //Выбираем её для использования
                    Log.NewLog(001, "Конфигурацию " + App.configuration.name + " изменили" + ((newConfiguration.name != App.configuration.name) ? (" на " + newConfiguration.name) : "")); // Это тернарный оператор. Я не хочу об этом говорить
                }
                updateInfoAboutConfig();

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        private void saveConfButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Trace.WriteLine("Готовимся изменять конфигурацию");

                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите изменить крнфигурацию.\nЭта операция необратима", "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
                Trace.WriteLine("result = " + result);

                if (result == MessageBoxResult.Yes)
                {
                    Configuration newConfiguration = new Configuration();
                    newConfiguration.name = ConfigurationCB.Text.ToString();
                    newConfiguration.themePath = themePathTB.Text;
                    newConfiguration.theme = themeTB.Text;
                    newConfiguration.assetsPath = assetsPathTB.Text;
                    newConfiguration.logPath = logPathTB.Text;
                    newConfiguration.SQLConnectionString = SQLConnectionStringTB.Text;
                    newConfiguration.serverIp = serverIpTB.Text;
                    newConfiguration.serverPort = serverPortTB.Text;
                    newConfiguration.netPath = netPathTB.Text;
                    newConfiguration.district = districtTB.Text;
                    newConfiguration.room = roomTB.Text;

                    // Проверка на неповторяемость имён конфигурации
                    // WARNING Очень криво и что то меняем
                    while (ConfigManage.HaveName(newConfiguration.name) && (newConfiguration.name != App.configuration.name))
                        newConfiguration.name = String.Concat(newConfiguration.name, " Дублёр");

                    Trace.WriteLine(App.configuration.name);
                    newConfiguration.TraceConfiguration();
                    ConfigManage.ChangeConfiguration(App.configuration.name, newConfiguration); //Изменяем конфигурацию в файле конфигурации
                    App.configuration = ConfigManage.GetConfiguration(newConfiguration.name); //Выбираем её для использования
                    Log.NewLog(001, "Конфигурацию " + App.configuration.name + " изменили" + ((newConfiguration.name != App.configuration.name) ? (" на " + newConfiguration.name) : "")); // Это тернарный оператор. Я не хочу об этом говорить
                }
                updateInfoAboutConfig();
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message);
                Log.NewLog(301, ex.Message);
            }
        }

        private int AddConfiguration()
        {
            try
            {
                Trace.WriteLine("Делаем новую конфигурацию");

                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите добавить новую крнфигурацию.\nЭта операция необратима", "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
                Trace.WriteLine("result = " + result);

                if (result == MessageBoxResult.Yes)
                {
                    Configuration newConfiguration = new Configuration();
                    newConfiguration = App.configuration;
                    //Вот сюда проверку впихни
                    //TODO Нужно зациклить
                    if (App.configuration.name == ConfigurationCB.Text.ToString())
                        newConfiguration.name = String.Concat(newConfiguration.name, "Дубль");
                    else
                        newConfiguration.name = ConfigurationCB.Text.ToString();

                    ConfigManage.SetConfiguration(newConfiguration); //добавляем новую конфигурацию
                    App.configuration = ConfigManage.GetConfiguration(newConfiguration.name); //Выбираем её для использования
                    Log.NewLog(001, "Добавили новую конфигурацию " + App.configuration.name);
                }
                updateInfoAboutConfig();

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        //TODO нужна проверка на неповторяймость названий конфигураций
        private void addConfButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Trace.WriteLine("Делаем новую конфигурацию");

                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите добавить новую крнфигурацию.\nЭта операция необратима", "Выберите один из вариантов", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
                Trace.WriteLine("result = " + result);

                if (result == MessageBoxResult.Yes)
                {
                    Configuration newConfiguration = new Configuration();
                    newConfiguration = App.configuration;
                    //Вот сюда проверку впихни
                    //TODO Нужно зациклить
                    if (App.configuration.name == ConfigurationCB.Text.ToString())
                        newConfiguration.name = String.Concat(newConfiguration.name, " Дубль");
                    else
                        newConfiguration.name = ConfigurationCB.Text.ToString();

                    ConfigManage.SetConfiguration(newConfiguration); //добавляем новую конфигурацию
                    App.configuration = ConfigManage.GetConfiguration(newConfiguration.name); //Выбираем её для использования
                    Log.NewLog(101, "Добавили новую конфигурацию " + App.configuration.name);
                }
                updateInfoAboutConfig();
            }
            catch(Exception ex)
            {
                Log.NewLog(301, "Ошибка при добавлении конфигурации " + ex.Message);
            }
        }
    }
}
