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
    

    class DBReturn
    {
        private MySqlConnection connection;
        private int isBorrowed;

        public void checkBorrowed(int id)
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");

            string checkQuery = "SELECT isBorrowed FROM books WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(checkQuery, connection);
            cmd.Parameters.AddWithValue("@id", id);

            //MessageBox.Show(id.ToString());
            try
            {
                connection.Open();
                MySqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    isBorrowed = int.Parse(rd["isBorrowed"].ToString());

                }

            }
            catch (Exception exception)
            {
                MessageBox.Show("대여현황 확인 실패.");
            }
            finally
            {
                connection.Close();
            }
        }


        public void returnBook(DataGridView bookDataGridView, DataGridView userDataGridView, int bookId, int userId, string bookIsbn)
        {
            checkBorrowed(bookId);
            if(isBorrowed == 0)
            {
                MessageBox.Show("대여중이지 않은 도서는 반납할수 없습니다.");
                return;
            }

            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");

            string bookReturnQuery = "UPDATE books SET userId = null, userName = null, isBorrowed = 0, borrowedAt = null, returnedAt = null WHERE id = @bookId";
            MySqlCommand bookCmd = new MySqlCommand(bookReturnQuery, connection);
            bookCmd.Parameters.AddWithValue("@bookId", bookId);

            string userReturnQuery = "UPDATE users SET borrowedNumber = borrowedNumber - 1 WHERE id = @userId";
            MySqlCommand userCmd = new MySqlCommand(userReturnQuery, connection);
            userCmd.Parameters.AddWithValue("@userId", userId);

            string historyReturnQuery = "UPDATE history SET returnedAt = @returnedAt WHERE userId = @userId AND bookIsbn = @bookIsbn";
            MySqlCommand historyCmd = new MySqlCommand(historyReturnQuery, connection);
            historyCmd.Parameters.AddWithValue("@returnedAt", DateTime.Now);
            historyCmd.Parameters.AddWithValue("@userId", userId);
            historyCmd.Parameters.AddWithValue("@bookIsbn", bookIsbn);


            try
            {
                connection.Open();

                bookCmd.ExecuteNonQuery();
                userCmd.ExecuteNonQuery();
                historyCmd.ExecuteNonQuery();

                DBBook dBBook = new DBBook();
                DBUser dBUser = new DBUser();
                dBBook.bookDataGridViewConnect(bookDataGridView);
                dBUser.userDataGridViewConnect(userDataGridView);

            } catch(Exception exception)
            {
                MessageBox.Show("반납하는데 실패.");
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
