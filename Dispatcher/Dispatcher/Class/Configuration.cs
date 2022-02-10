using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Class
{
    public class Configuration
    {
        public string name { get; set; }

        public string themePath { get; set; }

        public string theme { get; set; }

        public string assetsPath { get; set; }

        public string logPath { get; set; }

        public string SQLConnectionString { get; set; }

        public string serverIp { get; set; }

        public string serverPort { get; set; }

        public string netPath { get; set; }

        public string district { get; set; }

        public string room { get; set; }

        public static string[] GetFields()
        {
            string[] fields = {"themePath", "theme", "assetsPath", "logPath", "SQLConnectionString", "serverIp", "serverPort", "netPath", "district", "room" };
            return fields;
        }

        public void TraceConfiguration()
        {
            Trace.WriteLine("Name: "+ name + "\n" + "themePath: " + themePath + "\n" + "theme: " + theme + "\n" +
                "assetsPath: " + assetsPath + "\n" + "logPath: " + logPath + "\n" +
                "SQLConnectionString: " + SQLConnectionString + "\n" + "server ip: " + serverIp + "\n" + "server port: " + serverPort + "\n" + "net path: " + netPath + "\n" +
                "district: " + district + "\n" + "room: " + room + "\n");
        }
    }
}
