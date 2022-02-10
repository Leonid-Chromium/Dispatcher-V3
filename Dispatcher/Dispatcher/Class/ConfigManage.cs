using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Diagnostics;

namespace Dispatcher.Class
{
    class ConfigManage
    {
        public static string StandartConfigurationAddres = "../../../Files/Configuration.xml";

        //Создание новой конфигурации
        //TODO переделай на циклы
        public static int SetConfiguration(Configuration newConfiguration, string configurationPath)
        {
            try
            {
                Trace.WriteLine("Ввод новой конфигурации");

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(configurationPath);

                XmlElement configurations = xDoc.DocumentElement;

                //создаём новый элемент configuration
                XmlElement configuration = xDoc.CreateElement("configuration");
                //создаем атрибут name для configuration
                XmlAttribute configurationNameAttr = xDoc.CreateAttribute("name");
                //создаём значение для атрибута Name
                XmlText configurationAttrValue = xDoc.CreateTextNode(newConfiguration.name);

                //создаём элементы соответствующие по названию полям конфигурации
                XmlElement themePath = xDoc.CreateElement("themePath");
                XmlElement theme = xDoc.CreateElement("theme");
                XmlElement assetsPath = xDoc.CreateElement("assetsPath");
                XmlElement logPath = xDoc.CreateElement("logPath");
                XmlElement SQLConnectionString = xDoc.CreateElement("SQLConnectionString");
                XmlElement serverIp = xDoc.CreateElement("serverIp");
                XmlElement serverPort = xDoc.CreateElement("serverPort");
                XmlElement netPath = xDoc.CreateElement("netPath");
                XmlElement district = xDoc.CreateElement("district");
                XmlElement room = xDoc.CreateElement("room");

                //начинаем запись
                themePath.InnerText = newConfiguration.themePath;
                configuration.AppendChild(themePath);

                theme.InnerText = newConfiguration.theme;
                configuration.AppendChild(theme);

                assetsPath.InnerText = newConfiguration.assetsPath;
                configuration.AppendChild(assetsPath);

                logPath.InnerText = newConfiguration.logPath;
                configuration.AppendChild(logPath);

                SQLConnectionString.InnerText = newConfiguration.SQLConnectionString;
                configuration.AppendChild(SQLConnectionString);

                serverIp.InnerText = newConfiguration.serverIp;
                configuration.AppendChild(serverIp);

                serverPort.InnerText = newConfiguration.serverPort;
                configuration.AppendChild(serverPort);

                netPath.InnerText = newConfiguration.netPath;
                configuration.AppendChild(netPath);

                district.InnerText = newConfiguration.district;
                configuration.AppendChild(district);

                room.InnerText = newConfiguration.room;
                configuration.AppendChild(room);


                //добавляем атрибуту Name значение
                configurationNameAttr.AppendChild(configurationAttrValue);
                //добавляем конфигурации атрибут
                configuration.Attributes.Append(configurationNameAttr);
                //добавляем новую конфигурацию в список  конфигураций
                configurations.AppendChild(configuration);
                //сохраняем документ
                xDoc.Save(configurationPath);

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public static int SetConfiguration(Configuration newConfiguration)
        {
            try
            {
                return SetConfiguration(newConfiguration, StandartConfigurationAddres);
            }
            catch
            {
                return 1;
            }
        }

        //выборка конфигурации по имени
        public static Configuration GetConfiguration(string name, string configurationPath)
        {
            Trace.WriteLine("Поиск начат");

            Configuration configurationOut = new Configuration();
            configurationOut.name = name;

            XmlDocument xDoc = new XmlDocument(); //Создаём экземпляр документа
            xDoc.Load(configurationPath); //Загружаем Стандартный config файл

            // получим корневой элемент
            XmlElement configurations = xDoc.DocumentElement;

            foreach (XmlElement configuration in configurations)
            {
                Trace.WriteLine(configuration.GetAttribute("name"));

                // Находим интересующию нас конфигурацию
                if (configuration.GetAttribute("name") == name)
                {
                    Trace.WriteLine("\t^Это1");

                    //Перебираем узлы конфигурации
                    foreach (XmlElement configurationChildNode in configuration.ChildNodes)
                    {
                        Trace.Write("\t|" + configurationChildNode.Name + ":\n\t|\t");

                        // если узел - theme
                        if (configurationChildNode.Name == "theme")
                        {
                            configurationOut.theme = configurationChildNode.InnerText;
                            Trace.WriteLine(configurationChildNode.InnerText);
                        }

                        // если узел - themePath
                        if (configurationChildNode.Name == "themePath")
                        {
                            configurationOut.themePath = configurationChildNode.InnerXml;
                            Trace.WriteLine(configurationChildNode.InnerXml);
                        }

                        // если узел - assetsPath
                        if (configurationChildNode.Name == "assetsPath")
                        {
                            configurationOut.assetsPath = configurationChildNode.InnerXml;
                            Trace.WriteLine(configurationChildNode.InnerXml);
                        }

                        // если узел - logPath
                        if (configurationChildNode.Name == "logPath")
                        {
                            configurationOut.logPath = configurationChildNode.InnerXml;
                            Trace.WriteLine(configurationChildNode.InnerXml);
                        }

                        // если узел - SQLConnectionString
                        if (configurationChildNode.Name == "SQLConnectionString")
                        {
                            configurationOut.SQLConnectionString = configurationChildNode.InnerXml;
                            Trace.WriteLine(configurationChildNode.InnerXml);
                        }

                        // если узел - serverIp
                        if (configurationChildNode.Name == "serverIp")
                        {
                            configurationOut.serverIp = configurationChildNode.InnerXml;
                            Trace.WriteLine(configurationChildNode.InnerXml);
                        }

                        // если узел - serverPort
                        if (configurationChildNode.Name == "serverPort")
                        {
                            configurationOut.serverPort = configurationChildNode.InnerXml;
                            Trace.WriteLine(configurationChildNode.InnerXml);
                        }

                        // если узел - netPath
                        if (configurationChildNode.Name == "netPath")
                        {
                            configurationOut.netPath = configurationChildNode.InnerXml;
                            Trace.WriteLine(configurationChildNode.InnerXml);
                        }

                        // если узел - district
                        if (configurationChildNode.Name == "district")
                        {
                            configurationOut.district = configurationChildNode.InnerXml;
                            Trace.WriteLine(configurationChildNode.InnerXml);
                        }

                        // если узел - room
                        if (configurationChildNode.Name == "room")
                        {
                            configurationOut.room = configurationChildNode.InnerXml;
                            Trace.WriteLine(configurationChildNode.InnerXml);
                        }
                    }
                }
            }
            Trace.WriteLine("Поиск закончен");
            return configurationOut;
        }

        public static Configuration GetConfiguration(string name)
        {
            return GetConfiguration(name, StandartConfigurationAddres);
        }

        //удаление конфигурации по имени
        public static int DeleteConfiguration(string name, string configurationPath)
        {
            try
            {
                Trace.WriteLine("Удаление конфигурации " + name);

                //создаём объект документа и загружем в него документ по адресу
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(configurationPath);
                //получаем корневой элемент
                XmlElement configurations = xDoc.DocumentElement;

                XmlNode delNode;

                //ищем конфигурацию по имени
                foreach (XmlElement configuration in configurations)
                {
                    Trace.WriteLine(configuration.GetAttribute("name"));

                    // Находим интересующию нас конфигурацию
                    if (configuration.GetAttribute("name") == name)
                    {
                        delNode = configuration;
                        //удаление указаного узла
                        configurations.RemoveChild(delNode);
                    }
                }

                //сохранение документа
                xDoc.Save(configurationPath);

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public static int DeleteConfiguration(string name)
        {
            try
            {
                return DeleteConfiguration(name, StandartConfigurationAddres);
            }
            catch
            {
                return 1;
            }
        }

        //изменение конфигурации по имени
        public static int ChangeConfiguration(string name, Configuration newConfiguration, string configurationPath)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(configurationPath);

                XmlElement configurations = xDoc.DocumentElement;

                foreach (XmlElement configuration in configurations)
                {
                    Trace.WriteLine(configuration.GetAttribute("name"));

                    if (configuration.GetAttribute("name") == name)
                    {
                        //создаем атрибут name для configuration
                        XmlAttribute configurationNameAttr = xDoc.CreateAttribute("name");
                        //создаём значение для атрибута Name
                        XmlText configurationAttrValue = xDoc.CreateTextNode(newConfiguration.name);
                        //добавляем атрибуту Name значение
                        configurationNameAttr.AppendChild(configurationAttrValue);
                        //добавляем конфигурации атрибут
                        configuration.Attributes.Append(configurationNameAttr);

                        //Перебираем узлы конфигурации
                        foreach (XmlElement configurationChildNode in configuration.ChildNodes)
                        {
                            Trace.Write("\t|" + configurationChildNode.Name + ":\n\t|\t");

                            // если узел - theme
                            if (configurationChildNode.Name == "theme")
                            {
                                configurationChildNode.InnerText = newConfiguration.theme;
                                Trace.WriteLine(configurationChildNode.InnerText);
                            }

                            // если узел - themePath
                            if (configurationChildNode.Name == "themePath")
                            {
                                configurationChildNode.InnerXml = newConfiguration.themePath;
                                Trace.WriteLine(configurationChildNode.InnerXml);
                            }

                            // если узел - assetsPath
                            if (configurationChildNode.Name == "assetsPath")
                            {
                                configurationChildNode.InnerXml = newConfiguration.assetsPath;
                                Trace.WriteLine(configurationChildNode.InnerXml);
                            }

                            // если узел - logPath
                            if (configurationChildNode.Name == "logPath")
                            {
                                configurationChildNode.InnerXml = newConfiguration.logPath;
                                Trace.WriteLine(configurationChildNode.InnerXml);
                            }

                            // если узел - SQLConnectionString
                            if (configurationChildNode.Name == "SQLConnectionString")
                            {
                                configurationChildNode.InnerXml = newConfiguration.SQLConnectionString;
                                Trace.WriteLine(configurationChildNode.InnerXml);
                            }

                            // если узел - serverIp
                            if (configurationChildNode.Name == "serverIp")
                            {
                                configurationChildNode.InnerXml = newConfiguration.serverIp;
                                Trace.WriteLine(configurationChildNode.InnerXml);
                            }

                            // если узел - serverPort
                            if (configurationChildNode.Name == "serverPort")
                            {
                                configurationChildNode.InnerXml = newConfiguration.serverPort;
                                Trace.WriteLine(configurationChildNode.InnerXml);
                            }

                            // если узел - netPath
                            if (configurationChildNode.Name == "netPath")
                            {
                                configurationChildNode.InnerXml = newConfiguration.netPath;
                                Trace.WriteLine(configurationChildNode.InnerXml);
                            }

                            // если узел - district
                            if (configurationChildNode.Name == "district")
                            {
                                configurationChildNode.InnerXml = newConfiguration.district;
                                Trace.WriteLine(configurationChildNode.InnerXml);
                            }

                            // если узел - room
                            if (configurationChildNode.Name == "room")
                            {
                                configurationChildNode.InnerXml = newConfiguration.room;
                                Trace.WriteLine(configurationChildNode.InnerXml);
                            }
                        }
                    }
                }

                //сохраняем документ
                xDoc.Save(configurationPath);

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public static int ChangeConfiguration(string name, Configuration newConfiguration)
        {
            try
            {
                return ChangeConfiguration(name, newConfiguration, StandartConfigurationAddres);
            }
            catch
            {
                return 1;
            }
        }

        //TODO Получение списка конфигураций
        public static List<string> GetAllConfigurationName(string configurationPath)
        {
            List<string> configurationsName = new List<string> { };

            Trace.WriteLine("Поиск начат");

            XmlDocument xDoc = new XmlDocument(); //Создаём экземпляр документа
            xDoc.Load(configurationPath); //Загружаем Стандартный config файл

            // получим корневой элемент
            XmlElement configurations = xDoc.DocumentElement;

            Trace.WriteLine("Ищем в конфигурация конфигурации"); //Могло быть хуже

            foreach(XmlElement configuration in configurations)
            {
                Trace.WriteLine(configuration.GetAttribute("name"));
                configurationsName.Add(configuration.GetAttribute("name"));
                //configurationsName.Append(configuration.GetAttribute("name").ToString());
            }
            //Проверка
            Trace.WriteLine("Начинаем проверку GetAllConfigurationName");
            Trace.WriteLine("В массиве " + configurationsName.Count() + " элементов");
            foreach (string configuration in configurationsName)
            {
                Trace.WriteLine(configuration);
            }
            Trace.WriteLine("Проверка закончена");
            return configurationsName;
        }

        public static List<string> GetAllConfigurationName()
        {
            return GetAllConfigurationName(StandartConfigurationAddres);
        }
    }
}
