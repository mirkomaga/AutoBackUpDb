using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBackUpDb
{
    public static class Program
    {
        //public static AutoBKForm frm;
        //public static DataBaseController dbc;

        static void Main(string[] args)
        {

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //using (AutoBKForm mainForm = new AutoBKForm())
            //{
            //    frm = mainForm;
            //    Application.Run(frm);
            //}

            //frm = null;
        }

        public static IList<string> eseguoBackUp()
        {
            IList<DbConfig> data = Controller.ReadConfig();

            IList<string> nomeBK = new List<string>();

            foreach (DbConfig db in data)
            {
                DataBaseController dbc = new DataBaseController(db);
                nomeBK.Add(dbc.nomeFile);
                dbc.Backup();
            }

            return nomeBK;
        }
    }
}
