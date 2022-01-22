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
        public App()
        {
            //Выполняется при запуске
            //Class.TestCore core = new Class.TestCore(); //Тестовое ядро запускающее 5 стандартных окон
            Class.Core core = new Class.Core(); //Запуск нового ядра
            core.OpenMainWindow();
        }
        
    }
}
