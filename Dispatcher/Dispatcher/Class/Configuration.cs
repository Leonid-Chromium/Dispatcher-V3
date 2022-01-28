using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Class
{
    class Configuration
    {
        public string Name { get; set; }

        public string themePath { get; set; }

        public string theme { get; set; }

        public string assetsPath { get; set; }

        public string logPath { get; set; }

        public string SQLConnectionString { get; set; }

        public string district { get; set; }

        public void TraceConfiguration()
        {
            Trace.WriteLine("Name: "+ Name + "\n" + "themePath: " + themePath + "\n" + "theme: " + theme + "\n" + "assetsPath: " + assetsPath + "\n" + "logPath: " + logPath + "\n" + "SQLConnectionString: " + SQLConnectionString + "\n" + "district: " + district + "\n");
        }
    }
}
