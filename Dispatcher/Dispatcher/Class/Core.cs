using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Dispatcher.Class
{
    class Core
    {
        public string version = "0.5";
        public string user { get; set; }

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
                testWindow.Show();

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public int OpenCascade(int count)
        {
            try
            {
                for (int i = 0; i < count; i++)
                {
                    OpenMainWindow();
                    //break;
                }

                return 0;
            }
            catch
            {
                return 1;
            }

        }

        public int TestConfig()
        {
            try
            {
                Config config = new Config();
                Configuration actualConfiguration = config.GetConfigV1("Standart");
                Trace.WriteLine("\n\n\n");
                actualConfiguration.TraceConfiguration();
                Configuration newConfiguration = new Configuration();
                newConfiguration.name = "Test2.4";
                newConfiguration.themePath = "test themePath";
                newConfiguration.assetsPath = "test assetsPath";
                newConfiguration.logPath = "test logPath";
                newConfiguration.theme = "test theme";
                newConfiguration.SQLConnectionString = "test sql";
                newConfiguration.district = "test district";
                config.SetConfigV2(newConfiguration);

                return 0;
            }
            catch
            {
                return 1;
            }
            
        }

        public int TestNet()
        {
            try
            {
                //Prostodich
                //net.Fun1();

                //Скачивает файл с сервера на указаный путь
                //net.DownloadFile("file://192.168.1.160/Crystal_monitoring/NewDispatcher/TestFile.txt", @"D:\File\TestDir\Test\File.txt");

                //Читает файл в Trace
                TestNetClass.streamFile("file://192.168.1.160/Crystal_monitoring/NewDispatcher/TestFile.txt");

                //Загружает файл на сервер с указаного пути
                TestNetClass.UploadFile("file://192.168.1.160/Crystal_monitoring/NewDispatcher/TestFile.txt", @"D:\File\TestDir\Test\File.txt");

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public int TestThread()
        {
            try
            {
                // получаем текущий поток
                Thread currentThread = Thread.CurrentThread;

                //получаем имя потока
                Trace.WriteLine($"Имя потока: {currentThread.Name}");
                currentThread.Name = "Метод Main";
                Trace.WriteLine($"Имя потока: {currentThread.Name}");

                Trace.WriteLine($"Запущен ли поток: {currentThread.IsAlive}");
                Trace.WriteLine($"Id потока: {currentThread.ManagedThreadId}");
                Trace.WriteLine($"Приоритет потока: {currentThread.Priority}");
                Trace.WriteLine($"Статус потока: {currentThread.ThreadState}");

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public int TestThread2()
        {
            try
            {
                // получаем текущий поток
                Thread currentThread = Thread.CurrentThread;

                AppDomain appDomain = Thread.GetDomain();
                return 0;
            }
            catch
            {
                return 1;
            }
            
        }
        
        public int TestThread3()
        {
            try
            {
                Process proc = Process.GetProcessesByName("Dispatcher")[0];
                Trace.WriteLine($"ID: {proc.Id}");

                ProcessThreadCollection processThreads = proc.Threads;

                foreach (ProcessThread thread in processThreads)
                {
                    Trace.WriteLine($"ThreadId: {thread.Id}  StartTime: {thread.StartTime}");
                }

                ProcessModuleCollection modules = proc.Modules;

                foreach (ProcessModule module in modules)
                {
                    Trace.WriteLine($"Name: {module.ModuleName}  MemorySize: {module.ModuleMemorySize}");
                }

                return 0;
            }
            catch
            {
                return 1;
            }
            
        }

        public Core()
        {
            //Выполняется при запуске
            Core core = this;
            user = "User";
        }
    }
}
