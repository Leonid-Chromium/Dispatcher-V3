using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Windows;

namespace Dispatcher.Class
{
    class TestJsonLogClass
    {
        public static int Serializer(Log log)
        {
            try
            {
                Trace.WriteLine("Начинаем сериализацию");
                var options = new JsonSerializerOptions
                {
                    //Включвает кирилицу
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    //Включает перенос строки поле ккаждого поля
                    //WriteIndented = true
                };
                using (FileStream fileStream = new FileStream(App.configuration.logPath, FileMode.OpenOrCreate))
                {
                    fileStream.Seek(0, SeekOrigin.End); // Перевод курсора в конец файла
                    fileStream.Write(Encoding.Default.GetBytes("\n")); // Добавление новой строуи
                    string json = JsonSerializer.Serialize(log, options); //Составляем строку
                    Trace.WriteLine(json);
                    fileStream.Write(Encoding.Default.GetBytes(json)); // записываем строку
                    fileStream.Close(); // Закрываем эту коробку пандоры
                }
                return 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 1;
            }
        }

        /*
        public static async Task Serializer(Log log)
        {
            // запускаем новый поток
            using (FileStream fileStream = new FileStream(App.configuration.logPath, FileMode.OpenOrCreate))
            {
                Trace.WriteLine("Начинаем сериализацию");
                var option = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                };
                //jsonString = JsonSerializer.Serialize(weatherForecast, options);
                fileStream.Seek(0, SeekOrigin.End); // Перевод курсора в конец файла
                fileStream.Write(Encoding.Default.GetBytes("\n")); // Добавление новой строуи
                string json = JsonSerializer.Serialize(log);
                Trace.WriteLine(json);
                fileStream.Write(Encoding.Default.GetBytes(json));
                //await JsonSerializer.SerializeAsync<Log>(fileStream, log); // записываем лог
                Trace.WriteLine("Сериализация прошла успешно");
            }
        }
        */

        public async Task Deserializer()
        {
            // чтение данных
            using (FileStream fs = new FileStream(@"D:\File\TestDir\Test\Log.json", FileMode.OpenOrCreate))
            {
                Trace.WriteLine("Начинаем десериализацию");
                Log restoredLog = await JsonSerializer.DeserializeAsync<Log>(fs);
                Log.LogTrace(restoredLog);
                Trace.WriteLine("Заканчиваем десериализацию");
            }
        }
    }
}
