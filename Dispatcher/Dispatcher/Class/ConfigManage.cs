using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Diagnostics;
using System.IO;

namespace Dispatcher.Class
{
    class ConfigManage
    {
        public static string standartConfigurationAddres = "../../../Files/Configuration.xml";

        //Создание новой конфигурации
        /// <summary>
        /// Создаёт новую конфигурацию <paramref name="newConfiguration"/> в файле <paramref name="configurationPath"/>
        /// </summary>
        /// <param name="newConfiguration"></param>
        /// <param name="configurationPath"></param>
        /// <returns></returns>
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

                //TODO переделай на циклы

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

                App.logger.NewLog(201, "Добавленна новая конфигурация " + newConfiguration.name);
                return 0;
            }
            catch(Exception ex)
            {
                App.logger.NewLog(400, "Ошибка при создании конфигурации " + newConfiguration.name + " " + ex.Message);
                return 1;
            }
        }

        /// <summary>
        /// Создаёт новую конфигурацию <paramref name="newConfiguration"/> в стандартном файле
        /// </summary>
        /// <param name="newConfiguration"></param>
        /// <returns></returns>
        public static int SetConfiguration(Configuration newConfiguration)
        {
            try
            {
                return SetConfiguration(newConfiguration, standartConfigurationAddres);
            }
            catch(Exception ex)
            {
                App.logger.NewLog(401, "Ошибка при создании конфигурации " + newConfiguration.name + " " + ex.Message);
                return 1;
            }
        }

        //выборка конфигурации по имени
        /// <summary>
        /// Возвращает конфигурацию с именем <paramref name="name"/> из файла <paramref name="configurationPath"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configurationPath"></param>
        /// <returns></returns>
        public static Configuration GetConfiguration(string name, string configurationPath)
        {
            Trace.WriteLine("Поиск начат");

            Configuration configurationOut = new Configuration();
            configurationOut.name = name;
            try
            {
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

                            //TODO Trace кривой

                            switch (configurationChildNode.Name)
                            {
                                case "theme":
                                    configurationOut.theme = configurationChildNode.InnerText;
                                    Trace.WriteLine(configurationChildNode.InnerText);
                                    break;

                                case "themePath":
                                    configurationOut.themePath = configurationChildNode.InnerXml;
                                    Trace.WriteLine(configurationChildNode.InnerXml);
                                    break;

                                case "assetsPath":
                                    configurationOut.assetsPath = configurationChildNode.InnerXml;
                                    Trace.WriteLine(configurationChildNode.InnerXml);
                                    break;

                                case "logPath":
                                    configurationOut.logPath = configurationChildNode.InnerXml;
                                    Trace.WriteLine(configurationChildNode.InnerXml);
                                    break;

                                case "SQLConnectionString":
                                    configurationOut.SQLConnectionString = configurationChildNode.InnerXml;
                                    Trace.WriteLine(configurationChildNode.InnerXml);
                                    break;

                                case "serverIp":
                                    configurationOut.serverIp = configurationChildNode.InnerXml;
                                    Trace.WriteLine(configurationChildNode.InnerXml);
                                    break;

                                case "serverPort":
                                    configurationOut.serverPort = configurationChildNode.InnerXml;
                                    Trace.WriteLine(configurationChildNode.InnerXml);
                                    break;

                                case "netPath":
                                    configurationOut.netPath = configurationChildNode.InnerXml;
                                    Trace.WriteLine(configurationChildNode.InnerXml);
                                    break;

                                case "district":
                                    configurationOut.district = configurationChildNode.InnerXml;
                                    Trace.WriteLine(configurationChildNode.InnerXml);
                                    break;

                                case "room":
                                    configurationOut.room = configurationChildNode.InnerXml;
                                    Trace.WriteLine(configurationChildNode.InnerXml);
                                    break;
                            }
                        }
                        App.logger.NewLog(202, "Конфигурация " + name + " найдена");
                    }
                }
                Trace.WriteLine("Поиск закончен");
            }
            catch(Exception ex)
            {
                App.logger.NewLog(400, "Ошибка при поиске конфигурации " + name + " " + ex.Message);
            }
            return configurationOut;
        }

        /// <summary>
        /// Возвращает конфигурацию с именем <paramref name="name"/> из стандартного файла
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Configuration GetConfiguration(string name)
        {
            return GetConfiguration(name, standartConfigurationAddres);
        }

        //удаление конфигурации по имени
        /// <summary>
        /// Удаляет конфигурацию с названием <paramref name="name"/> из файла <paramref name="configurationPath"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configurationPath"></param>
        /// <returns></returns>
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

                App.logger.NewLog(200, "Конфигурация " + name + " удалена");

                return 0;
            }
            catch(Exception ex)
            {
                App.logger.NewLog(400, "Ошибка при удалении конфигурации " + name + " " + ex.Message);
                return 1;
            }
        }

        /// <summary>
        /// Удаляет конфигурацию с названием <paramref name="name"/> из стандартного файла
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int DeleteConfiguration(string name)
        {
            try
            {
                return DeleteConfiguration(name, standartConfigurationAddres);
            }
            catch(Exception ex)
            {
                App.logger.NewLog(401, "Ошибка в ConfigManage.DeleteConfiguration " + ex.Message);
                return 1;
            }
        }

        //изменение конфигурации по имени
        /// <summary>
        /// Заменяет конфигурацию с именем <paramref name="name"/> из файла <paramref name="configurationPath"/> на конфигурацию <paramref name="newConfiguration"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="newConfiguration"></param>
        /// <param name="configurationPath"></param>
        /// <returns></returns>
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

                            //TODO Переделай на switch( Наверное )

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

                        //TODO переопредели ещё один метод без выхода после нахождения первой подходящей конфигурации
                        //сохраняем документ
                        xDoc.Save(configurationPath);

                        App.logger.NewLog(201, "Изменена конфигурация " + name);

                        return 0;
                    }
                }

                //сохраняем документ
                xDoc.Save(configurationPath);
                return 0;
            }
            catch (Exception ex)
            {
                App.logger.NewLog(400, "Ошибка в ConfigManage.ChangeConfiguration " + ex.Message);
                return 1;
            }
        }

        /// <summary>
        /// Заменяет конфигурацию с именем <paramref name="name"/> из стандартного файла на конфигурацию <paramref name="newConfiguration"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="newConfiguration"></param>
        /// <returns></returns>
        public static int ChangeConfiguration(string name, Configuration newConfiguration)
        {
            try
            {
                return ChangeConfiguration(name, newConfiguration, standartConfigurationAddres);
            }
            catch (Exception ex)
            {
                App.logger.NewLog(401, "Ошибка в ConfigManage.ChangeConfiguration " + ex.Message);
                return 1;
            }
        }

        // Получение списка конфигураций
        /// <summary>
        /// Возвращает Список всех доступных конфигураций по указанному адресу <paramref name="configurationPath"/>
        /// </summary>
        /// <param name="configurationPath"></param>
        /// <returns></returns>
        public static List<string> GetAllConfigurationName(string configurationPath)
        {
            List<string> configurationsName = new List<string> { };

            Trace.WriteLine("Поиск начат");

            XmlDocument xDoc = new XmlDocument(); //Создаём экземпляр документа
            xDoc.Load(configurationPath); //Загружаем Стандартный config файл

            // получим корневой элемент
            XmlElement configurations = xDoc.DocumentElement;

            Trace.WriteLine("Ищем в конфигурация конфигурации"); //Могло быть хуже //Это я об этом изречении

            foreach(XmlElement configuration in configurations)
            {
                //Trace.WriteLine(configuration.GetAttribute("name"));
                if (configuration.Name != "SavedConfiguration")
                {
                    configurationsName.Add(configuration.GetAttribute("name"));
                }
            }

            //Проверка
            Trace.WriteLine("Начинаем проверку GetAllConfigurationName");
            Trace.WriteLine("В массиве " + configurationsName.Count() + " элементов");
            foreach (string configuration in configurationsName)
                Trace.WriteLine(configuration);
            Trace.WriteLine("Проверка закончена");

            return configurationsName;
        }

        /// <summary>
        /// Возвращает Список всех доступных конфигураций по стандартному адрессу адресу
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllConfigurationName()
        {
            return GetAllConfigurationName(standartConfigurationAddres);
        }

        //Проверка на повторяемость имени
        /// <summary>
        /// Ищет конфигурации с именем равным <paramref name="name"/> в документе по адрессу <paramref name="configurationPath"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configurationPath"></param>
        /// <returns> <c>true</c> если найденно, <c>false</c> если не найдено</returns>
        public static bool HaveName(string name, string configurationPath)
        {
            try
            {
                List<string> configurationsName = GetAllConfigurationName(configurationPath);
                foreach(string configurationName in configurationsName)
                    if (configurationName == name)
                        return true;
                return false;
            }
            catch (Exception ex)
            {
                App.logger.NewLog(405, "Ошибка в ConfigManage.HaveName " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Ищет конфигурации с именем равным <paramref name="name"/> в документе по стандартному адрессу
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HaveName(string name)
        {
            return HaveName(name, standartConfigurationAddres);
        }

        //Получение последней конфигурации
        /// <summary>
        /// Возвращение последней конфигурации по адрессу <paramref name="configurationPath"/>
        /// </summary>
        /// <param name="configurationPath"></param>
        /// <returns></returns>
        public static string GetSavedConfiguration(string configurationPath)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument(); //Создаём экземпляр документа
                xDoc.Load(configurationPath); //Загружаем Стандартный config файл

                XmlElement configurations = xDoc.DocumentElement;

                foreach(XmlElement node in configurations)
                {
                    if(node.Name == "SavedConfiguration")
                    {
                        return node.InnerText;
                    }
                }

                App.logger.NewLog(300, "Не найдена сохранённая операция ConfigManage.GetSavedConfiguration");
                return string.Empty;
            }
            catch(Exception ex)
            {
                App.logger.NewLog(400, "Не найдена сохранённая операция ConfigManage.GetSavedConfiguration " + ex.Message);
                Trace.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Возвращение последней конфигурации
        /// </summary>
        /// <returns></returns>
        public static string GetSavedConfiguration()
        {
            return GetSavedConfiguration(standartConfigurationAddres);
        }

        //Запись последней конфигурации
        /// <summary>
        /// Запись <paramref name="configurationName"> как последняя конфигурация, в файл по адрессу <paramref name="configurationPath"/>
        /// </summary>
        /// <param name="configurationName"> Название конфигурации </param>
        /// <param name="configurationPath"> Путь к файлу конфигурации </param>
        /// <returns></returns>
        public static int SetSavedConfiguration(string configurationName, string configurationPath)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument(); //Создаём экземпляр документа
                xDoc.Load(configurationPath); //Загружаем Стандартный config файл

                XmlElement configurations = xDoc.DocumentElement;

                foreach (XmlElement node in configurations)
                {
                    if (node.Name == "SavedConfiguration")
                    {
                        //Trace.WriteLine("нашли");
                        node.InnerText = configurationName;
                        App.logger.NewLog(200, "Записали конфигурацию для старта");
                    }
                }

                xDoc.Save(configurationPath);

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// Запись <paramref name="configurationName"> как последняя конфигурация
        /// </summary>
        /// <param name="configurationName"></param>
        /// <returns></returns>
        public static int SetSavedConfiguration(string configurationName)
        {
            return SetSavedConfiguration(configurationName, standartConfigurationAddres);
        }

        //Проверка наличия или создания каталога
        /// <summary>
        /// Проверяет существует директория по указанному пути и создаёт её если не было
        /// </summary>
        /// <param name="LogPath"></param>
        /// <returns><c>0</c> если <paramref name="LogPath"/> существует,<c>1</c> если <paramref name="LogPath"> не существует, но был создан, <c>2</c> если произошла ошибка</returns>
        public static int CheckLogPath(string LogPath)
        {
            try
            {
                if (Directory.Exists(LogPath))
				{
                    App.logger.NewLog(100, "Директория для логов найдена");
                    return 0;
                }
                else
                {
                    Directory.CreateDirectory(LogPath);
                    App.logger.NewLog(101, "Директория для логов не найдена, но создана");
                    return 1;
                }
            }
            catch (Exception ex)
            {
                App.logger.NewLog(400, "ConfigManage.CheckLogPath " + ex.Message);
                return 2;
            }

        }

        //Создание файла лога
        /// <summary>
        /// Создаёт файл лога в директории по указанному пути
        /// </summary>
        /// <param name="LogPath">Адрес директории в которой будет создан файл логов</param>
        /// <returns></returns>
        public static int MakeLogFile(string LogPath)
        {
            try
			{
                DateTime dateTime = DateTime.Now;
                string newLogFile = String.Concat(dateTime.Date.ToShortDateString(), ".", dateTime.Hour, ".", dateTime.Minute, ".", dateTime.Second, ".json");
                File.Create(String.Concat(LogPath, newLogFile));
                App.logFile = newLogFile;
                App.logger.NewLog(100, "Файл для лога создан");
            }
            catch (Exception ex)
			{
                App.logger.NewLog(400, "ConfigManage.MakeLogFile " + ex.Message);
			}
            return 0;
        }
    }
}
