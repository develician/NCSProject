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

        public void insertUser(DataGridView dataGridView, string name, string phone)
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none;CharSet=UTF8");
            string insertQuery = "INSERT INTO users (name, phone) VALUES (@name, @phone)";
            MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@phone", phone);

            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();

                userDataGridViewConnect(dataGridView);


            }
            catch (Exception exception)
            {
                MessageBox.Show("유저를 추가하는데 실패.");
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public int getUserCount()
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");
            string countQuery = "SELECT COUNT(*) FROM users";
            MySqlCommand cmd = new MySqlCommand(countQuery, connection);

            int userCount = 0;
            try
            {
                connection.Open();

                userCount = int.Parse(cmd.ExecuteScalar().ToString());

            }
            catch (Exception exception)
            {
                MessageBox.Show("유저의 수를 불러오는데 실패.");
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return userCount;
        }

        public void removeSelectedUser(DataGridView dataGridView, int id)
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");
            string removeQuery = "DELETE FROM users WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(removeQuery, connection);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                userDataGridViewConnect(dataGridView);


            }
            catch (Exception exception)
            {
                MessageBox.Show("유저를 삭제하는데 실패.");
            }
            finally
            {
                connection.Close();
            }
        }

        public void updateUserInfo(int userId, string name, string phone)
        {

            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none;CharSet=UTF8");
            string updateQuery = "UPDATE users SET name = @name, phone = @phone WHERE id = @userId";
            MySqlCommand cmd = new MySqlCommand(updateQuery, connection);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@userId", userId);


            try
            {
                connection.Open();

                cmd.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                MessageBox.Show("유저 정보 수정 실패.");
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Models.User> searchUser(int searchComboIndex, string searchWord)
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none;CharSet=UTF8");

            string searchQuery = "";

            if (searchComboIndex == 0)
            {
                searchQuery = "SELECT * FROM users WHERE name LIKE '%" + searchWord + "%'";

            }
            else if (searchComboIndex == 1)
            {
                searchQuery = "SELECT * FROM users WHERE phone LIKE '%" + searchWord + "%'";
            }

            MySqlCommand cmd = new MySqlCommand(searchQuery, connection);

            List<Models.User> searchedUsersList = new List<Models.User>();
            try
            {
                connection.Open();

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Models.User user = new Models.User();
                    user.id = int.Parse(rd["id"].ToString());
                    user.name = rd["name"].ToString();
                    user.phone = rd["phone"].ToString();
                    user.borrowedNumber = int.Parse(rd["borrowedNumber"].ToString());
                    user.delayedCnt = int.Parse(rd["delayedCnt"].ToString());

                    searchedUsersList.Add(user);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("사용자 정보 검색 실패.");
            }
            finally
            {
                connection.Close();
            }

            return searchedUsersList;
        }
    }
}
