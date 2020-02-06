using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokOto
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>C:\Users\LENOVO\Desktop\proje\StokOto\StokOto\Program.cs
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Anamenu());
        }

    }
}
