﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Class
{
    class Core
    {
        public int OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
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
        public Core()
        {
            //Выполняется при запуске
            Config config = new Config();
            Configuration actualConfiguration = config.GetConfigV1("Castom");
            actualConfiguration.TraceConfiguration();
            //Configuration newConfiguration = new Configuration();
            //newConfiguration.Name = "Test";
            //newConfiguration.settingsPath = "dfsgdh";
            //config.SetConfigV1(newConfiguration);
        }
    }
}
