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

        public int SetConfigV2(Configuration newConfiguration, string configurationPath)
        {
            Trace.WriteLine("Ввод новой конфигурации");

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(configurationPath);

            XmlElement configurations = xDoc.DocumentElement;

            //создаём новый элемент configuration
            XmlElement configuration = xDoc.CreateElement("configuration");
            //создаем атрибут name для configuration
            XmlAttribute configurationNameAttr = xDoc.CreateAttribute("Name");
            //создаём значение для атрибута Name
            XmlText configurationAttrValue = xDoc.CreateTextNode(newConfiguration.name);

            // создаём новый элемент locations
            XmlElement locations = xDoc.CreateElement("locations");

            // создаём новый элемент themePathLocation
            XmlElement themePathLocation = xDoc.CreateElement("location");
            // создаем атрибут name для themePathLocation
            XmlAttribute themePathLocationNameAttr = xDoc.CreateAttribute("Name");
            //создаём значение для атрибута Name
            XmlText themePathLocationNameAttrValue = xDoc.CreateTextNode("themePath");

            // создаём новый элемент assetsPathLocation
            XmlElement assetsPathLocation = xDoc.CreateElement("location");
            // создаем атрибут name для assetsPathLocation
            XmlAttribute assetsPathLocationNameAttr = xDoc.CreateAttribute("Name");
            //создаём значение для атрибута Name
            XmlText assetsPathLocationNameAttrValue = xDoc.CreateTextNode("assetsPath");

            // создаём новый элемент logPathLocation
            XmlElement logPathLocation = xDoc.CreateElement("location");
            // создаем атрибут name для logPathLocation
            XmlAttribute logPathLocationNameAttr = xDoc.CreateAttribute("Name");
            //создаём значение для атрибута Name
            XmlText logPathLocationNameAttrValue = xDoc.CreateTextNode("logPath");

            // создаём новый элемент settings, theme, SQLConnectionString и district
            XmlElement settings = xDoc.CreateElement("settings");
            XmlElement theme = xDoc.CreateElement("theme");
            XmlElement SQLConnectionString = xDoc.CreateElement("SQLConnectionString");
            XmlElement district = xDoc.CreateElement("district");

            //начинаем запись
            //заполняем атрибуты Name
            themePathLocationNameAttr.AppendChild(themePathLocationNameAttrValue);
            assetsPathLocationNameAttr.AppendChild(assetsPathLocationNameAttrValue);
            logPathLocationNameAttr.AppendChild(logPathLocationNameAttrValue);
            //даём атрибуты Name
            themePathLocation.Attributes.Append(themePathLocationNameAttr);
            assetsPathLocation.Attributes.Append(assetsPathLocationNameAttr);
            logPathLocation.Attributes.Append(logPathLocationNameAttr);
            //заполняем location - ы
            themePathLocation.InnerText = newConfiguration.themePath;
            assetsPathLocation.InnerText = newConfiguration.assetsPath;
            logPathLocation.InnerText = newConfiguration.logPath;
            //заполняем locations
            locations.AppendChild(themePathLocation);
            locations.AppendChild(assetsPathLocation);
            locations.AppendChild(logPathLocation);

            //задаём значения theme, SQLConnectionString и district
            theme.InnerText = newConfiguration.theme;
            SQLConnectionString.InnerText = newConfiguration.SQLConnectionString;
            district.InnerText = newConfiguration.district;
            //заполняем settings
            settings.AppendChild(theme);
            settings.AppendChild(SQLConnectionString);
            settings.AppendChild(district);

            //добавляем в конфигурацию locations и settings
            configuration.AppendChild(locations);
            configuration.AppendChild(settings);
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

        public int SetConfigV2(Configuration newConfiguration)
        {
            return SetConfigV2(newConfiguration, StandartConfigAddres);
        }

        //public int SetConfigV1(Configuration newConfiguration, string configurationPath)
        //{
        //    Trace.WriteLine("Ввод новой конфигурации");

        //    XmlDocument xDoc = new XmlDocument();
        //    xDoc.Load(configurationPath);

        //    XmlElement xRoot = xDoc.DocumentElement;

        //    // создаем новый элемент configuration
        //    XmlElement configuration = xDoc.CreateElement("configuration");
        //    // создаем атрибут name для configuration
        //    XmlAttribute configurationNameAttr = xDoc.CreateAttribute("Name");

        //    // создаём новый элемент locations
        //    XmlElement locations = xDoc.CreateElement("locations");
        //    // создаём новый элемент location
        //    XmlElement location = xDoc.CreateElement("location");
        //    // создаем атрибут name для location
        //    XmlAttribute locationNameAttr = xDoc.CreateAttribute("Name");

        //    // создаём новый элемент settings
        //    XmlElement settings = xDoc.CreateElement("settings");
        //    // создаём новый элемент theme
        //    XmlElement theme = xDoc.CreateElement("theme");
        //    // создаём новый элемент theme
        //    XmlElement SQLConnectionString = xDoc.CreateElement("SQLConnectionString");
        //    // создаём новый элемент district
        //    XmlElement district = xDoc.CreateElement("district");

        //    // создаём текстовое значение для названий атрибутов
        //    XmlText configurationAttrValue = xDoc.CreateTextNode(newConfiguration.Name);
        //    XmlText themePathAttrName = xDoc.CreateTextNode("themePath");
        //    XmlText assetsPathAttrName = xDoc.CreateTextNode("assetsPath");
        //    XmlText logPathAttrName = xDoc.CreateTextNode("logPath");

        //    // создаём текстовое значение элементов
        //    XmlText themePathValue = xDoc.CreateTextNode(newConfiguration.themePath);
        //    XmlText assetsPathValue = xDoc.CreateTextNode(newConfiguration.assetsPath);
        //    XmlText logPathValue = xDoc.CreateTextNode(newConfiguration.logPath);

        //    XmlText themeValue = xDoc.CreateTextNode(newConfiguration.theme);
        //    XmlText SQLConnectionStringValue = xDoc.CreateTextNode(newConfiguration.SQLConnectionString);
        //    XmlText districtValue = xDoc.CreateTextNode(newConfiguration.district);

        //    //добавляем узлы
        //    location.AppendChild(themePathValue);
        //    themePathAttrName = themePathValue;
        //    location.Attributes.Append(themePathAttrName);
        //    location.AppendChild(settingsPath);
        //    locationNameAttr.AppendChild(locationName);
        //    location.Attributes.Append(locationNameAttr);
        //    locations.AppendChild(location);
        //    configuration.AppendChild(locations);
        //    configurationNameAttr.AppendChild(configurationName);
        //    configuration.Attributes.Append(configurationNameAttr);
        //    xRoot.AppendChild(configuration);
        //    xDoc.Save(configurationPath);

        //    return 0;
        //}

        //public int SetConfigV0(Configuration newConfiguration, string configurationPath)
        //{
        //    Trace.WriteLine("Ввод новой конфигурации");
        //    Trace.WriteLine("Название: " + newConfiguration.Name);
        //    Trace.WriteLine("Местонахождение файла настроек: " + newConfiguration.settingsPath);

        //    XmlDocument xDoc = new XmlDocument();
        //    xDoc.Load(configurationPath);

        //    XmlElement xRoot = xDoc.DocumentElement;

        //    // создаем новый элемент configuration
        //    XmlElement configuration = xDoc.CreateElement("configuration");
        //    // создаем атрибут name для configuration
        //    XmlAttribute configurationNameAttr = xDoc.CreateAttribute("Name");
        //    // создаём новый элемент locations
        //    XmlElement locations = xDoc.CreateElement("locations");
        //    // создаём новый элемент location
        //    XmlElement location = xDoc.CreateElement("location");
        //    // создаем атрибут name для location
        //    XmlAttribute locationNameAttr = xDoc.CreateAttribute("Name");
        //    // создаём текстовое значчение для элементов и атрибутов
        //    XmlText configurationName = xDoc.CreateTextNode(newConfiguration.Name);
        //    XmlText locationName = xDoc.CreateTextNode("settings");
        //    XmlText settingsPath = xDoc.CreateTextNode(newConfiguration.settingsPath);

        //    //добавляем узлы
        //    location.AppendChild(settingsPath);
        //    locationNameAttr.AppendChild(locationName);
        //    location.Attributes.Append(locationNameAttr);
        //    locations.AppendChild(location);
        //    configuration.AppendChild(locations);
        //    configurationNameAttr.AppendChild(configurationName);
        //    configuration.Attributes.Append(configurationNameAttr);
        //    xRoot.AppendChild(configuration);
        //    xDoc.Save(configurationPath);

        //    return 0;
        //}

        //public int SetConfigV1(Configuration newConfiguration)
        //{
        //    return SetConfigV1(newConfiguration, StandartConfigAddres);
        //}

        public Configuration GetConfigV1(string Name, string configurationPath)
        {
            Trace.WriteLine("Поиск начат");

            Configuration configurationOut = new Configuration();
            configurationOut.name = Name;

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
                    foreach (XmlElement configurationChildNode in configuration.ChildNodes)
                    {
                        Trace.WriteLine("\t" + configurationChildNode.Name);

                        // если узел - locations
                        if (configurationChildNode.Name == "locations")
                        {
                            Trace.WriteLine("\t\t^Это2");

                            //Перебираем местонахождения
                            foreach (XmlElement location in configurationChildNode)
                            {
                                Trace.WriteLine("\t\t" + location.GetAttribute("Name"));

                                //Если атрибут Name равен themePath
                                if (location.GetAttribute("Name") == "themePath")
                                {
                                    Trace.WriteLine("\t\t\t^Это");
                                    configurationOut.themePath = location.InnerText;
                                    Trace.WriteLine("\t\t\t" + location.InnerText);
                                }

                                //Если атрибут Name равен assetsPath
                                if (location.GetAttribute("Name") == "assetsPath")
                                {
                                    Trace.WriteLine("\t\t\t^Это");
                                    configurationOut.assetsPath = location.InnerText;
                                    Trace.WriteLine("\t\t\t" + location.InnerText);
                                }

                                //Если атрибут Name равен logPath
                                if (location.GetAttribute("Name") == "logPath")
                                {
                                    Trace.WriteLine("\t\t\t^Это");
                                    configurationOut.logPath = location.InnerText;
                                    Trace.WriteLine("\t\t\t" + location.InnerText);
                                }
                            }
                        }

                        // если узел - settings
                        if(configurationChildNode.Name == "settings")
                        {
                            Trace.WriteLine("\t\t^Это2");

                            //перебираем настройки
                            foreach (XmlElement setting in configurationChildNode)
                            {
                                Trace.WriteLine("\t\t" + setting.Name);

                                //если Name равен theme
                                if(setting.Name == "theme")
                                {
                                    Trace.WriteLine("\t\t\t^Это");
                                    configurationOut.theme = setting.InnerText;
                                    Trace.WriteLine("\t\t\t" + setting.InnerText);
                                }

                                //если Name равен SQLConnectionString
                                if (setting.Name == "SQLConnectionString")
                                {
                                    Trace.WriteLine("\t\t\t^Это");
                                    configurationOut.SQLConnectionString = setting.InnerText;
                                    Trace.WriteLine("\t\t\t" + setting.InnerText);
                                }

                                //если Name равен location
                                if (setting.Name == "district")
                                {
                                    Trace.WriteLine("\t\t\t^Это");
                                    configurationOut.district = setting.InnerText;
                                    Trace.WriteLine("\t\t\t" + setting.InnerText);
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
