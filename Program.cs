using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _2_курс_мониторинг_частоты_использованяи_приложения
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
