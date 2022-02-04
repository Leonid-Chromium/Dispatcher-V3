using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dispatcher.Class
{
    class TestJsonLogClass
    {
        public int serialJson()
        {
            Log log = new Log(this, 0, "testLog");
            string json = JsonSerializer.Serialize<Log>(log);
            Trace.WriteLine(json);
            Log restoredLog = JsonSerializer.Deserialize<Log>(json);
            restoredLog.LogTrace();

            return 0;
        }

        public async Task Serializer(Log log)
        {
            // сохранение данных
            using (FileStream fs = new FileStream(@"D:\File\TestDir\Test\Log.json", FileMode.OpenOrCreate))
            {
                Trace.WriteLine("Начинаем сериализацию");
                fs.Seek(0, SeekOrigin.End);
                await JsonSerializer.SerializeAsync<Log>(fs, log);
                fs.Write(Encoding.Default.GetBytes(@"
"));
                Trace.WriteLine("Data has been saved to file");
            }
        }

        public async Task Deserializer()
        {
            // чтение данных
            using (FileStream fs = new FileStream(@"D:\File\TestDir\Test\Log.json", FileMode.OpenOrCreate))
            {
                Trace.WriteLine("Начинаем десериализацию");
                Log restoredLog = await JsonSerializer.DeserializeAsync<Log>(fs);
                restoredLog.LogTrace();
                Trace.WriteLine("Заканчиваем десериализацию");
            }
        }
    }
}
