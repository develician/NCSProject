using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace BookManager.DBUtils
{
    
    class DBUser
    {
        private MySqlConnection connection;
        public DBUser()
        {

        }

        public void userDataGridViewConnect(DataGridView dataGridView)
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");
            string selectQuery = "SELECT * FROM users;";
            try
            {
                connection.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView.DataSource = dt;

            }
            catch (Exception exception)
            {
                MessageBox.Show("유저 관리 DB연동 실패.");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
