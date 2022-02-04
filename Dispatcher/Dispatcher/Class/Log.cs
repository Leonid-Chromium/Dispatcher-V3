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
        public string level { get; set; }
        public DateTime dateTime { get; set; }
        public string version { get; set; }
        public uint code { get; set; }
        public string user { get; set; }
        public string message { get; set; }

        //public Core core { get; set; }

        public Log(object sendler, uint code, string message)
        {
            switch (code % 100)
            {
                case 0:
                    level = "debug";
                    break;
            }

            dateTime = DateTime.Now;

            Core core = (Core)sendler;

            version = core.version;

            this.code = code;

            user = core.user;

            this.message = message;

            LogTrace();
            TestJsonLogClass test = new TestJsonLogClass();
            Task taskS = test.Serializer(this);
        }

        public void LogTrace()
        {
            Trace.WriteLine("Level: " + level);
            Trace.WriteLine("Data-time: " + dateTime);
            Trace.WriteLine("Version: " + version);
            Trace.WriteLine("Code: " + code);
            Trace.WriteLine("User: " + user);
            Trace.WriteLine("Message: " + message);
        }
    }
}
