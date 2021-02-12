using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBackUpDb
{
    class DataBaseController
    {
        private string ip;
        private string user;
        private string password;
        private string dbName;
        public string nomeFile;
        private string path;
        private int delay;

        public DataBaseController(DbConfig c)
        {
            ip = c.ip;
            user = c.user;
            password = c.password;
            dbName = c.name;
            path = "X:\\mirko\\bkpDb\\" + dbName;
            delay = c.delay * 60000;

            // QUI CICLO PER TEMPO IL BACKUP

            DateTime saveNow = DateTime.Now;

            nomeFile = dbName + saveNow.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_")+".sql";
        }

        public async Task startTimeDelay()
        {

            Console.WriteLine("Eseguo Back-up: " + DateTime.Now.ToString());

            //Program.frm.label2.Text = DateTime.Now.ToString();
            //Program.frm.Refresh();

            Backup();

            await Task.Delay(this.delay);

            //startTimeDelay();
        }

        public void Backup()
        {
            string constring = "server= " + this.ip + "; database =" + this.dbName + "; username= " + this.user + "; password="+this.password+";";
            
            string file = this.path+"\\"+this.nomeFile;
            
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportInfo.AddCreateDatabase = true;
                        mb.ExportToFile(file);
                        conn.Close();
                    }
                }
            }
        }

        private void Restore()
        {
            string constring = "server=localhost;user=root;pwd=qwerty;database=test;";
            string file = "C:\\backup.sql";
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportFromFile(file);
                        conn.Close();
                    }
                }
            }
        }

        private void SetStartup()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            rk.SetValue(Application.ProductName, Application.ExecutablePath);
        }
    }
}