using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GenerateExtension.AgentClassGenerator
{
    public class ConfigHelper
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public string MainPage { get; set; }

        private static readonly string _configName = "_cfg.xml";

        public void Init(string slnPath)
        {
            string filePath = Path.Combine(slnPath, _configName);
            using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write,
             FileShare.None))
            {
                XmlSerializer xmlserializer = new XmlSerializer(typeof(ConfigHelper));
                xmlserializer.Serialize(stream, this);
            }
        }

        public static ConfigHelper Load(string slnPath)
        {
            string filePath = Path.Combine(slnPath, _configName);
            if (!File.Exists(filePath))
            {
                return null;
            }
            try
            {
                using (FileStream readstream = new FileStream(filePath, FileMode.Open, FileAccess.Read,
                FileShare.Read))
                {
                    XmlSerializer xmlserializer = new XmlSerializer(typeof(ConfigHelper));
                    var info = (ConfigHelper)xmlserializer.Deserialize(readstream);
                    return info;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
