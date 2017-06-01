using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            MainForm f = new MainForm();
            f.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            f.ShowDialog();
        }
    }
}
