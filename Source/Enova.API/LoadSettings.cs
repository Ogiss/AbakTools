using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace Enova.API
{
    public class LoadSettings
    {
        // Fields
        public string[] Installations = new string[0];

        // Methods
        public static LoadSettings Load()
        {
            LoadSettings settings = new LoadSettings();
            if (File.Exists(FileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(FileName);
                XmlNodeList list = document.SelectNodes("LoadSettings/Installations/Installation/text()");
                settings.Installations = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    settings.Installations[i] = list[i].InnerText;
                }
            }
            return settings;
        }

        public void Save()
        {
            string fileName = FileName;
            string filename = Path.ChangeExtension(fileName, ".temp");
            XmlDocument document = new XmlDocument();
            XmlElement newChild = document.CreateElement("LoadSettings");
            document.AppendChild(newChild);
            XmlElement element2 = document.CreateElement("Installations");
            newChild.AppendChild(element2);
            foreach (string str3 in this.Installations)
            {
                XmlElement element3 = document.CreateElement("Installation");
                element3.InnerText = str3;
                element2.AppendChild(element3);
            }
            document.Save(filename);
            File.Delete(fileName);
            File.Move(filename, fileName);
        }

        // Properties
        private static string FileName
        {
            get
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Soneta");
                Directory.CreateDirectory(path);
                return Path.Combine(path, "Modules.xml");
            }
        }
    }


}
