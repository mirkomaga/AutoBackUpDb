using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace AutoBackUpDb
{
    class DataBaseController
    {
        private string ip;
        private string user;
        private string password;
        private string dbName;
        
        public DataBaseController(DbConfig c)
        {
            //ip = c.ip;
            //user = c.name;
            //password = c.user;
            //dbName = c.password;

            // QUI CICLO PER TEMPO IL BACKUP

        }

        private void Backup()
        {
            string constring = "server="+this.ip+";user="+ this.user +";pwd=qwerty;database="+ this.dbName +";";
            string file = "C:\\backup.sql";
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
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
    }
}