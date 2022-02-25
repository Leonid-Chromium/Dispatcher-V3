using Dispatcher.Class;
using System;
using System.Collections.Generic;
//using System.Configuration;
///NOTE Если будут ошибки связаные с "Configuration" раскоментируй соответсвующий юзинг
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Dispatcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        

        public static string version = "0.5";
        public static string user { get; set; } = "Unknown";

        public static Class.Configuration configuration { get; set; }

        public int OpenMainWindow()
        {
            try
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public int OpenTestWindow()
        {
            try
            {
                Windows.TestWindow testWindow = new Windows.TestWindow();
                testWindow.parent = this;
                testWindow.Show();

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public App()
        {
            //Выполняется при запуске
            //TODO Продумай случай если нет файла или конфигураций в файле
            try
            {
                configuration = Class.ConfigManage.GetConfiguration(ConfigManage.GetSavedConfiguration());
            }
            catch
            {
                List<string> configurationsName = ConfigManage.GetAllConfigurationName();
                configuration = Class.ConfigManage.GetConfiguration(configurationsName.First());
            }
            //TODO Нужен лог
            configuration.TraceConfiguration();
            OpenTestWindow();
        }

    }
}
