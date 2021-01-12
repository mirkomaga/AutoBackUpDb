using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBackUpDb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IList<DbConfig> data = Controller.ReadConfig();

            foreach (DbConfig db in data)
            {
                DataBaseController dbc = new DataBaseController(db);
            }
        }
    }

    class Controller
    {
        public static IList<DbConfig> ReadConfig()
        {
            IList<DbConfig> res = new List<DbConfig>();

            try
            {
                string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\db.json";
                JArray data = JArray.Parse(@File.ReadAllText(path));

                res = data.ToObject<IList<DbConfig>>();
            }
            catch
            {
                MessageBox.Show("Non ho trovato il file di configurazione");
            }

            return res;
        }
    }
}
