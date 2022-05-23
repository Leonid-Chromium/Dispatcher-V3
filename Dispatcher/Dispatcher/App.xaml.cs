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

        public static string _roleStr = "";
        public static string roleStr
        {
            get
			{
                return _roleStr;
			}
            set
			{
                _roleStr = value;
                logger.user = _roleStr;
                logger.NewLog(200, "Выбрана другая роль");
			}
        }

        public static int role;

        public static Class.Configuration configuration { get; set; }
        public static string logFile { get; set; } = "UnknownLog";

        //Настройка позволяющая делать запись сканером без определения человека
        public static bool UnknownUserMode = true;
        public static bool UnknownDistrictMode = false;

        public static Logger logger = new Logger();

        /// <summary>
        /// Открываем главное окно
        /// </summary>
        /// <returns></returns>
        public static int OpenMainWindow()
        {
            try
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Workspace.AccessCheck();
                mainWindow.Title = roleStr;
				mainWindow.Show();

                logger.NewLog(200, "Открываем главное окно");

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// Открываем тестовое окно
        /// </summary>
        /// <returns></returns>
        public static int OpenTestWindow()
        {
            try
            {
                Windows.TestWindow testWindow = new Windows.TestWindow();
                testWindow.Show();

                logger.NewLog(200, "Открываем тестовое окно");

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// Открываем окно авторизации
        /// </summary>
        /// <returns></returns>
        public static int OpenAuthorizationWindow()
        {
            try
            {
                Windows.AuthorizationWindow authorizationWindow = new Windows.AuthorizationWindow();
                authorizationWindow.Show();

                logger.NewLog(200, "Открываем окно авторизации");

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
            //TODO Продумай случай если нет файла конфигураций
            logger.version = version;
            logger.user = user;
            logger.NewLog(200, "Конфигурация ещё не загруженна");
            try
            {
                configuration = Class.ConfigManage.GetConfiguration(ConfigManage.GetSavedConfiguration());
            }
            catch
            {
                List<string> configurationsName = ConfigManage.GetAllConfigurationName();
                configuration = Class.ConfigManage.GetConfiguration(configurationsName.First());
                logger.NewLog(300, "Не найдена сохранённая конфигурация");
            }
            configuration.TraceConfiguration();
            ConfigManage.CheckLogPath(App.configuration.logPath);
            ConfigManage.MakeLogFile(App.configuration.logPath);

            logger.NewLog(200, "Загрузили конфигурацию");

            logger.logPath = String.Concat(App.configuration.logPath, logFile);
            logger.flag = true;
            logger.TryStart();
            logger.NewLog(200, "Инициализированли логер");
            
            OpenAuthorizationWindow();
        }

    }
}
