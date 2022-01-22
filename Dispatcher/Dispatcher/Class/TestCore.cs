using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Class
{
    class TestCore
    {
        public void NewMainWindow()
        {
            for (int i = 0; i < 5; i++)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }

        public TestCore()
        {
            NewMainWindow();
        }
    }
}
