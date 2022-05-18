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

using LogLib;
using System.IO;

namespace Dispatcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        public static string version = "3.6";
        public static string user { get; set; } = "Unknown";

        public static string roleStr = "";

        public static int role;

        public static Class.Configuration configuration { get; set; }
        public static string logFile { get; set; } = "UnknownLog";

        //Настройка позволяющая делать запись сканером без определения человека
        public static bool UnknownUserMode = true;
        public static bool UnknownDistrictMode = false;

        public static int OpenMainWindow()
        {
            try
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Workspace.AccessCheck();
                mainWindow.Title = roleStr;
				mainWindow.Show();

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public static int OpenTestWindow()
        {
            try
            {
                Windows.TestWindow testWindow = new Windows.TestWindow();
                testWindow.Show();

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public static int OpenAuthorizationWindow()
        {
            try
            {
                Windows.AuthorizationWindow authorizationWindow = new Windows.AuthorizationWindow();
                authorizationWindow.Show();
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        

        public App()
        {
            Trace.WriteLine(typeof(UCs.CalendarUC));
            //Выполняется при запуске
            //TODO Продумай случай если нет файла конфигураций
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
            ConfigManage.CheckLogPath(App.configuration.logPath);
            ConfigManage.MakeLogFile(App.configuration.logPath);
            Logger logger = new Logger(version, user, String.Concat(App.configuration.logPath, logFile));
            logger.NewLog(100, "Загрузили конфигурацию");
            OpenAuthorizationWindow();
        }

    }
}
