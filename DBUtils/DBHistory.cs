using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace BookManager
{
    class DBHistory
    {
        private MySqlConnection connection;
        public DBHistory()
        {
           
        }

        public void removeAllHistory(DataGridView dataGridView)
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");
            string removeQuery = "DELETE FROM history";
            string selectQuery = "SELECT * FROM history;";
            try
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(removeQuery, connection);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView.DataSource = dt;
            } catch(Exception exception)
            {
                MessageBox.Show("기록 삭제 실패.");
            } finally
            {
                connection.Close();
            }
        }

        public void historyGridViewConnect(DataGridView dataGridView)
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");
            string selectQuery = "SELECT * FROM history;";
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
                MessageBox.Show("기록 관리 DB연동 실패.");
            }
            finally
            {
                connection.Close();
            }
        }

        public void removeSelectedHistory(DataGridView dataGridView)
        {

            try
            {
                if (dataGridView.SelectedCells.Count > 0)
                {
                    int selectedRowIndex = dataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView.Rows[selectedRowIndex];
                    
                    int historyId = int.Parse(selectedRow.Cells["id"].Value.ToString());

                    connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");

                    connection.Open();

                    string removeQuery = "DELETE FROM history WHERE id = @historyId";

                    MySqlCommand cmd = new MySqlCommand(removeQuery, connection);
                    cmd.Parameters.AddWithValue("historyId", historyId);
                    cmd.ExecuteNonQuery();
                    historyGridViewConnect(dataGridView);
                }
            } catch (Exception exception)
            {
                MessageBox.Show("이상한곳을 클릭하셨나요?");
            } finally
            {
                if(connection != null)
                {
                    connection.Close();
                }
            }
        }
    }
}
