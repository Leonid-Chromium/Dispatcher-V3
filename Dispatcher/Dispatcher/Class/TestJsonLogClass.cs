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
            Log log = new Log(0, "testLog");
            string json = JsonSerializer.Serialize<Log>(log);
            Trace.WriteLine(json);
            Log restoredLog = JsonSerializer.Deserialize<Log>(json);
            restoredLog.LogTrace();

            return 0;
        }

        public static async Task Serializer(Log log)
        {
            // запускаем новый поток
            using (FileStream fileStream = new FileStream(@"D:\File\TestDir\Test\Log.json", FileMode.OpenOrCreate))
            {
                Trace.WriteLine("Начинаем сериализацию");
                fileStream.Seek(0, SeekOrigin.End); // Перевод курсора в конец файла
                fileStream.Write(Encoding.Default.GetBytes("\n")); // Добавление новой строуи
                await JsonSerializer.SerializeAsync<Log>(fileStream, log); // записываем лог
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
