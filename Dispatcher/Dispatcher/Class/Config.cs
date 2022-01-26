using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Diagnostics;

namespace Dispatcher.Class
{
    class Config
    {
        public string StandartConfigAddres = "../../../Files/Config.xml";

        public int SetConfigV1(Configuration newConfiguration, string configurationPath)
        {
            Trace.WriteLine("Ввод новой конфигурации");
            Trace.WriteLine("Название: " + newConfiguration.Name);
            Trace.WriteLine("Местонахождение файла настроек: " + newConfiguration.settingsPath);

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(configurationPath);

            XmlElement xRoot = xDoc.DocumentElement;

            // создаем новый элемент configuration
            XmlElement configuration = xDoc.CreateElement("configuration");
            // создаем атрибут name для configuration
            XmlAttribute configurationNameAttr = xDoc.CreateAttribute("Name");
            // создаём новый элемент locations
            XmlElement locations = xDoc.CreateElement("locations");
            // создаём новый элемент location
            XmlElement location = xDoc.CreateElement("location");
            // создаем атрибут name для location
            XmlAttribute locationNameAttr = xDoc.CreateAttribute("Name");
            // создаём текстовое значчение для элементов и атрибутов
            XmlText configurationName = xDoc.CreateTextNode(newConfiguration.Name);
            XmlText locationName = xDoc.CreateTextNode("settings");
            XmlText settingsPath = xDoc.CreateTextNode(newConfiguration.settingsPath);

            //добавляем узлы
            location.AppendChild(settingsPath);
            locationNameAttr.AppendChild(locationName);
            location.Attributes.Append(locationNameAttr);
            locations.AppendChild(location);
            configuration.AppendChild(locations);
            configurationNameAttr.AppendChild(configurationName);
            configuration.Attributes.Append(configurationNameAttr);
            xRoot.AppendChild(configuration);
            xDoc.Save(configurationPath);

            return 0;
        }

        public int SetConfigV1(Configuration newConfiguration)
        {
            return SetConfigV1(newConfiguration, StandartConfigAddres);
        }

        public Configuration GetConfigV1(string Name, string configurationPath)
        {
            Trace.WriteLine("Поисск начат");
            Configuration configurationOut = new Configuration();
            configurationOut.Name = Name;

            XmlDocument xDoc = new XmlDocument(); //Создаём экземпляр документа
            xDoc.Load(configurationPath); //Загружаем Стандартный config файл

            // получим корневой элемент
            XmlElement configurations = xDoc.DocumentElement;

            foreach (XmlElement configuration in configurations)
            {
                Trace.WriteLine(configuration.GetAttribute("Name"));

                // Находим интересующию нас конфигурацию
                if (configuration.GetAttribute("Name") == Name)
                {
                    Trace.WriteLine("\t^Это1");

                    //Перебираем узлы конфигурации
                    foreach (XmlElement configurationChildnode in configuration.ChildNodes)
                    {
                        Trace.WriteLine("\t" + configurationChildnode.Name);

                        // если узел - locations
                        if (configurationChildnode.Name == "locations")
                        {
                            Trace.WriteLine("\t\t^Это2");

                            //Перебираем местонахождения
                            foreach (XmlElement location in configurationChildnode)
                            {
                                Trace.WriteLine("\t\t" + location.GetAttribute("Name"));

                                //Если атрибут Name равен settings
                                if (location.GetAttribute("Name") == "settings")
                                {
                                    Trace.WriteLine("\t\t\t^Это");
                                    configurationOut.settingsPath = location.InnerText;
                                    Trace.WriteLine("\t\t\t" + location.InnerText);
                                }
                            }
                        }
                    }
                }
            }
            Trace.WriteLine("Поиск закончен");
            return configurationOut;
        }

        public Configuration GetConfigV1(string Name)
        {
            return GetConfigV1(Name, StandartConfigAddres);
        }
    }
}
