using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Class
{
    class Log
    {
        public static string level { get; set; }
        public static DateTime dateTime { get; set; }
        public static string version { get; set; }
        public static uint code { get; set; }
        public static string user { get; set; }
        public static string message { get; set; }

        //public Core core { get; set; }

        public Log(uint ncode, string nmessage)
        {
            switch (code % 100)
            {
                case 0:
                    level = "debug";
                    break;
            }

            dateTime = DateTime.Now;

            version = App.version;

            code = ncode;

            user = App.user;

            message = nmessage;

            LogTrace();
            Task taskS = TestJsonLogClass.Serializer(this);
            taskS.Start();
        }

        public int LogTrace()
        {
            try
            {
                Trace.WriteLine("Level: " + level);
                Trace.WriteLine("Data-time: " + dateTime);
                Trace.WriteLine("Version: " + version);
                Trace.WriteLine("Code: " + code);
                Trace.WriteLine("User: " + user);
                Trace.WriteLine("Message: " + message);

                return 0;
            }
            catch
            {
                return 1;
            }
        }
    }
}
