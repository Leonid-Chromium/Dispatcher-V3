using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dispatcher.Class
{
    class TestClass
    {
        public string StandartConfigAddres = "../../../Files/Config.xml";

        public void ViewConfig()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(StandartConfigAddres);
            Trace.WriteLine("Файл загружен");

            XmlElement element = xDoc.DocumentElement;
            Trace.WriteLine(element.Name);

            testfun(element);
        }

        public void testfun(XmlElement element)
        {
            if (element.HasAttribute("Name"))
            {
                Trace.WriteLine(element.GetAttribute("Name"));
            }
            if (element.HasChildNodes)
                if(element.InnerXml != null)
                foreach (XmlElement xmlElement in element.ChildNodes)
                {
                    Trace.WriteLine(xmlElement.Name);
                    testfun(xmlElement);
                }
        }
    }
}
