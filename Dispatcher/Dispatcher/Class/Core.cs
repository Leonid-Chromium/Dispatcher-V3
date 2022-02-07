using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Class
{
    class Core
    {
        public string version = "0.5";
        public string user { get; set; }

        public int OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            return 0;
        }

        public int OpenTestWindow()
        {
            Windows.TestWindow testWindow = new Windows.TestWindow();
            testWindow.Show();
            return 0;
        }

        public void OpenCascade(int count)
        {
            for (int i = 0; i < count; i++)
            {
                OpenMainWindow();
                //break;
            }

        }

        public void TestConfig()
        {
            Config config = new Config();
            Configuration actualConfiguration = config.GetConfigV1("Standart");
            Trace.WriteLine("\n\n\n");
            actualConfiguration.TraceConfiguration();
            Configuration newConfiguration = new Configuration();
            newConfiguration.Name = "Test2.4";
            newConfiguration.themePath = "test themePath";
            newConfiguration.assetsPath = "test assetsPath";
            newConfiguration.logPath = "test logPath";
            newConfiguration.theme = "test theme";
            newConfiguration.SQLConnectionString = "test sql";
            newConfiguration.district = "test district";
            config.SetConfigV2(newConfiguration);
        }

        public int TestNet()
        {
            TestNetClass net = new TestNetClass();

            //Prostodich
            //net.Fun1();

            //Скачивает файл с сервера
            //net.DownloadFile("file://192.168.1.160/Crystal_monitoring/NewDispatcher/TestFile.txt", @"D:\File\TestDir\Test\File.txt");

            //Читает файл в Trace
            net.streamFile("file://192.168.1.160/Crystal_monitoring/NewDispatcher/TestFile.txt");

            //Загружает файл на сервер
            net.UploadFile("file://192.168.1.160/Crystal_monitoring/NewDispatcher/TestFile.txt", @"D:\File\TestDir\Test\File.txt");

            return 0;
        }

        public int TestLog()
        {
            Log log = new Log(this, 0, "testLog2");
            TestJsonLogClass json = new TestJsonLogClass();
            return 0;
        }
        
        public Core()
        {
            //Выполняется при запуске
            Core core = this;
            user = "User";

            OpenMainWindow();
        }
    }
}
