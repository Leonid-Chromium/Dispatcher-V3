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

        public Log()
        {

        }

        public static int NewLog(uint icode, string imessage)
        {
            try
            {
                string ilevel = "";
                switch (icode / 100)
                {
                    case 0:
                        ilevel = "Debug";
                        break;
                        /*
                        * Сообщения отладки, профилирования.
                        * В production системе обычно сообщения этого уровня включаются при первоначальном запуске системы или для поиска узких мест (bottleneck-ов).
                        */

                    case 1:
                        ilevel = "Info";
                        break;
                        /*
                        * Обычные сообщения, информирующие о действиях системы.
                        * Реагировать на такие сообщения вообще не надо, но они могут помочь, например, при поиске багов, расследовании интересных ситуаций итд.
                        */

                    case 2:
                        ilevel = "Warn";
                        break;
                        /*
                        * Записывая такое сообщение, система пытается привлечь внимание обслуживающего персонала.
                        * Произошло что-то странное. Возможно, это новый тип ситуации, ещё не известный системе.
                        * Следует разобраться в том, что произошло, что это означает, и отнести ситуацию либо к инфо-сообщению, либо к ошибке. Соответственно, придётся доработать код обработки таких ситуаций.
                        */

                    case 3:
                        ilevel = "Error";
                        break;
                        /*
                        * Ошибка в работе системы, требующая вмешательства. Что-то не сохранилось, что-то отвалилось. Необходимо принимать меры довольно быстро!
                        * Ошибки этого уровня и выше требуют немедленной записи в лог, чтобы ускорить реакцию на них.
                        * Нужно понимать, что ошибка пользователя – это не ошибка системы. Если пользователь ввёл в поле -1, где это не предполагалось – не надо писать об этом в лог ошибок.
                        */

                    case 4:
                        ilevel = "Fatal";
                        break;
                        /*
                         * это особый класс ошибок. Такие ошибки приводят к неработоспособности системы в целом, или неработоспособности одной из подсистем.
                         * Чаще всего случаются фатальные ошибки из-за неверной конфигурации или отказов оборудования. Требуют срочной, немедленной реакции.
                         * Возможно, следует предусмотреть уведомление о таких ошибках по SMS.
                         */
                }
                Log log = new Log {level =  ilevel,
                    dateTime = DateTime.Now,
                    version = App.version,
                    code = icode,
                    user = App.user,
                    message = imessage
                };

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
                Trace.WriteLine("Level: \t\t" + log.level);
                Trace.WriteLine("Data-time: \t\t" + log.dateTime);
                Trace.WriteLine("Version: \t\t" + log.version);
                Trace.WriteLine("Code: \t\t" + log.code);
                Trace.WriteLine("User: \t\t" + log.user);
                Trace.WriteLine("Message: \t\t" + log.message);

                return 0;
            }
            catch
            {
                return 1;
            }
        }
    }
}
