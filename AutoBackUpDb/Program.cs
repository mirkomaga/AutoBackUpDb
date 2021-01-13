using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBackUpDb
{
    static class Program
    {
        public static Form1 frm;

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (Form1 mainForm = new Form1())
            {
                frm = mainForm;
                Application.Run(frm);
            }
            frm = null;
        }
    }
}
