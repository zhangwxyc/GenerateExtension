using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateExtension
{
    public class SlnInfo
    {
        public string SlnPath { get; set; }
        public string SelectedDir { get; set; }

        public string NameSpace
        {
            get
            {
                return SelectedDir.Replace(SlnPath, "").Trim('\\').Replace("\\", ".");
            }
        }
        public string SelectProjectDir { get; set; }
    }
}
