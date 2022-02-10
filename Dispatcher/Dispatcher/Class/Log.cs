using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Class
{
    [Serializable]
    class Log
    {
        public string level { get; set; }
        public DateTime dateTime { get; set; }
        public string version { get; set; }
        public uint code { get; set; }
        public string user { get; set; }
        public string message { get; set; }

        //WARNING надо сделать конструктор без параметров
        //public Log(uint ncode, string nmessage)
        //{
        //    switch (ncode / 100)
        //    {
        //        case 0:
        //            level = "debug";
        //            break;
        //    }

        //    dateTime = DateTime.Now;

        //    version = App.version;

        //    code = ncode;

        //    user = App.user;

        //    message = nmessage;

        //    LogTrace();
        //}

        public Log()
        {

        }

        //WARNING  Я чуствую здесь подвох.
        public static int NewLog(uint icode, string imessage)
        {
            try
            {
                string ilevel = "";
                switch (icode / 100)
                {
                    case 0:
                        ilevel = "debug";
                        break;
                }
                Log log = new Log {level =  ilevel,
                    dateTime = DateTime.Now,
                    version = App.version,
                    code = icode,
                    user = App.user,
                    message = imessage
                };


                //log.dateTime = DateTime.Now;
                //log.version = App.version;
                //log.code = icode;
                //log.user = App.user;
                //log.message = imessage;

                Task task = Task.Run(() => TestJsonLogClass.Serializer(log));
                
                Trace.WriteLine("Сериализация закончена");
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public static int LogTrace(Log log)
        {
            try
            {
                Trace.WriteLine("Level: " + log.level);
                Trace.WriteLine("Data-time: " + log.dateTime);
                Trace.WriteLine("Version: " + log.version);
                Trace.WriteLine("Code: " + log.code);
                Trace.WriteLine("User: " + log.user);
                Trace.WriteLine("Message: " + log.message);

                return 0;
            }
            catch
            {
                return 1;
            }
        }
    }
}
