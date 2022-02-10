using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Class
{
    class TestNetClass
    {
        //Дич какая-то
        public static int Fun1()
        {
            IPHostEntry host1 = Dns.GetHostEntry("www.microsoft.com");
            Trace.WriteLine(host1.HostName);
            foreach (IPAddress ip in host1.AddressList)
                Trace.WriteLine(ip.ToString());

            Trace.WriteLine("");

            IPHostEntry host2 = Dns.GetHostEntry("google.com");
            Trace.WriteLine(host2.HostName);
            foreach (IPAddress ip in host2.AddressList)
                Trace.WriteLine(ip.ToString());

            return 0;
        }

        public static int DownloadFile(string address, string fileName)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadFile(address, fileName);
                Trace.WriteLine("Файл загружен");

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public static int streamFile(string fileAddress)
        {
            try
            {
                WebRequest request = WebRequest.Create(fileAddress);
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line = "";
                        while ((line = reader.ReadLine()) != null)
                        {
                            Trace.WriteLine(line);
                        }
                    }
                }
                response.Close();
                Trace.WriteLine("Запрос выполнен");

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public static int UploadFile(string address, string fileName)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.UploadFile(address, fileName);
                Trace.WriteLine("Файл отправлен");

                return 0;
            }
            catch
            {
                return 1;
            }
        }
    }
}
