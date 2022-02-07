using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        public App()
        {
            //Выполняется при запуске

        }
        
    }
}
