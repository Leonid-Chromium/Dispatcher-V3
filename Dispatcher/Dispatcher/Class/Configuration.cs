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

        public string settingsPath { get; set; }

        public void TraceConfiguration()
        {
            Trace.WriteLine("Name: "+ Name + "\nsettingsPath: " + settingsPath);
        }
    }
}
