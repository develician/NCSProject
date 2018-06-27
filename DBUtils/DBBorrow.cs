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
    class DBBorrow
    {
        private MySqlConnection connection;

        private int isBorrowed;
        private int tooManyBorrowed;

        public DBBorrow()
        {


        }

     
        public void checkTooManyBorrowed(int id)
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");

            string selectQuery = "SELECT borrowedNumber FROM users WHERE id = @userId";
            MySqlCommand cmd = new MySqlCommand(selectQuery, connection);
            cmd.Parameters.AddWithValue("@userId", id);

            try
            {
                connection.Open();

                MySqlDataReader rd = cmd.ExecuteReader();

                if(rd.Read())
                {
                    if(int.Parse(rd["borrowedNumber"].ToString()) >= 3)
                    {
                        tooManyBorrowed = 1;
                    }
                }

            } catch(Exception exception)
            {
                MessageBox.Show("몇권을 빌렸는지 가져오는데 실패.");
            } finally
            {
                connection.Close();
            }
        }


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

                if(rd.Read())
                {
                    isBorrowed = int.Parse(rd["isBorrowed"].ToString());
                    
                }
                
            } catch(Exception exception)
            {
                MessageBox.Show("대여현황 확인 실패.");
            } finally
            {
                connection.Close();
            }
        }

        public void borrow(DataGridView bookDataGridView, DataGridView userDataGridView, int userId, string userName, int bookId, string bookName, string bookIsbn)
        {
            checkTooManyBorrowed(userId);
            if(tooManyBorrowed == 1)
            {
                MessageBox.Show("한번에 3권이상은 대여불가입니다.");
                return;
            }
            checkBorrowed(bookId);
            if(isBorrowed == 1)
            {
                MessageBox.Show("이미 대여중인 도서는 대여가 불가능합니다.");
                return;
            }
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");
            string borrowBookQuery = "UPDATE books SET userId = @userId, userName = @userName, isBorrowed = 1, borrowedAt = @borrowedAt, returnedAt = @returnedAt WHERE id = @bookId";
            MySqlCommand bookCmd = new MySqlCommand(borrowBookQuery, connection);
            bookCmd.Parameters.AddWithValue("@userId", userId);
            bookCmd.Parameters.AddWithValue("@userName", userName);
            bookCmd.Parameters.AddWithValue("@borrowedAt", DateTime.Now);
            bookCmd.Parameters.AddWithValue("@returnedAt", DateTime.Now.AddDays(7));
            bookCmd.Parameters.AddWithValue("@bookId", bookId);

            string borrowUserQuery = "UPDATE users SET borrowedNumber = borrowedNumber + 1 WHERE id = @userId";
            MySqlCommand userCmd = new MySqlCommand(borrowUserQuery, connection);
            userCmd.Parameters.AddWithValue("@userId", userId);

            string borrowHistoryQuery = "INSERT INTO history (userId, userName, borrowedAt, bookName, bookIsbn) VALUES (@userId, @userName, @borrowedAt, @bookname, @bookIsbn)";
            MySqlCommand historyCmd = new MySqlCommand(borrowHistoryQuery, connection);
            historyCmd.Parameters.AddWithValue("@userId", userId);
            historyCmd.Parameters.AddWithValue("@userName", userName);
            historyCmd.Parameters.AddWithValue("@borrowedAt", DateTime.Now);
            historyCmd.Parameters.AddWithValue("@bookName", bookName);
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
                MessageBox.Show("책을 대여하는데 실패.");
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
