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
    class DBBook
    {
        private MySqlConnection connection;
        private int isExisting = 0;
        private int isBorrowed;
        public DBBook()
        {

        }

        public void bookDataGridViewConnect(DataGridView dataGridView)
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");

            string selectQuery = "SELECT * FROM books;";
            

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
                MessageBox.Show("책 데이터 불러오기 실패.");
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public void checkExistingIsbn(string isbn)
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");
            string checkQuery = "SELECT * FROM books WHERE isbn = @isbn";
            MySqlCommand cmd = new MySqlCommand(checkQuery, connection);
            cmd.Parameters.AddWithValue("@isbn", isbn);
            
            try
            {
                connection.Open();
                MySqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    MessageBox.Show("이미 존재하는 Isbn입니다.");
                    isExisting = 1;
                    return;
                } else
                {
                    isExisting = 0;
                }

            } catch(Exception exception)
            {
                MessageBox.Show("isbn을 체크하는 도중 오류 발생.");
            } finally
            {
                connection.Close();
            }
        }

        public void insertNewBook(DataGridView dataGridView, string isbn, string name, string publisher, int page)
        {

            checkExistingIsbn(isbn);
            if(isExisting == 1)
            {
                return;
            }

            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none;CharSet=UTF8");
            string insertQuery = "INSERT INTO books (isbn, name, publisher, page) VALUES (@isbn, @name, @publisher, @page);";
            MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
            cmd.Parameters.AddWithValue("@isbn", isbn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@publisher", publisher);
            cmd.Parameters.AddWithValue("@page", page);



            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                bookDataGridViewConnect(dataGridView);
            }
            catch (Exception exception)
            {
                MessageBox.Show("새 책을 추가하는데 실패했습니다.");
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public int getBookCount()
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");

            string countQuery = "SELECT COUNT(*) FROM books";
            MySqlCommand cmd = new MySqlCommand(countQuery, connection);

            int count = 0;
            try
            {
                connection.Open();

                count = int.Parse(cmd.ExecuteScalar().ToString());

                
            } catch(Exception exception)
            {
                MessageBox.Show("책의 개수를 가져오는데 실패했습니다.");
            } finally
            {
                connection.Close();
            }

            return count;
        }

        public List<Models.Book> getSearchedBooks(int searchIndex, string searchWord)
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none;CharSet=UTF8");

            string searchQuery = "";

            if (searchIndex == 0)
            {
                searchQuery = "SELECT * FROM books WHERE isbn LIKE '%" + searchWord +  "%'";

            } else if(searchIndex == 1)
            {
                searchQuery = "SELECT * FROM books WHERE name LIKE '%" + searchWord + "%'";
            } else if (searchIndex == 2)
            {
                searchQuery = "SELECT * FROM books WHERE publisher LIKE '%" + searchWord + "%'";
            }

            MySqlCommand cmd = new MySqlCommand(searchQuery, connection);
            //cmd.Parameters.AddWithValue("@searchWord", searchWord);


            List<Models.Book> searchedBooksList = new List<Models.Book>();
            try
            {
                connection.Open();

                MySqlDataReader rd = cmd.ExecuteReader();

                while(rd.Read())
                {
                    Models.Book book = new Models.Book();
                    book.id = int.Parse(rd["id"].ToString());
                    book.isbn = rd["isbn"].ToString();
                    book.name = rd["name"].ToString();
                    book.publisher = rd["publisher"].ToString();
                    book.page = int.Parse(rd["page"].ToString());

                    // null check
                    if (!rd.IsDBNull(5))
                    {
                        book.userId = int.Parse(rd["userId"].ToString());
                    }

                    if (!rd.IsDBNull(6))
                    {
                        book.userName = rd["userName"].ToString();
                    }
                    if (!rd.IsDBNull(7))
                    {
                        book.isBorrowed = int.Parse(rd["isBorrowed"].ToString());
                    }
                    if (!rd.IsDBNull(8))
                    {
                        //book.borrowedAt = DateTime.ParseExact(rd["borrowedAt"].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        book.borrowedAt = Convert.ToDateTime(rd["borrowedAt"]);
                    }
                    if (!rd.IsDBNull(9))
                    {
                        //book.returnedAt = DateTime.ParseExact(rd["returnedAt"].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        book.returnedAt = Convert.ToDateTime(rd["returnedAt"]);
                    }

                    //
                    //
                    //
                    searchedBooksList.Add(book);
                }

                

            }
            catch (Exception exception)
            {
                MessageBox.Show("검색된 책 리스트를 불러오는데 실패.");
            }
            finally
            {
                connection.Close();
            }

            return searchedBooksList;
        }

        public void updateBookInfo(int id, string isbn, string name, string publisher, int page)
        {
            
            //checkExistingIsbn(isbn);
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none;CharSet=UTF8");

            try
            {
                connection.Open();

                string updateQuery = "UPDATE books SET isbn = @isbn, name = @name, publisher = @publisher, page = @page WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(updateQuery, connection);
                cmd.Parameters.AddWithValue("@isbn", isbn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@publisher", publisher);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();




            }
            catch (Exception exception)
            {
                MessageBox.Show("책 정보를 수정하는데 실패.");
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

        public void removeSelectedBook(DataGridView dataGridView, int id)
        {
            checkBorrowed(id);
            if(isBorrowed == 1)
            {
                MessageBox.Show("대여중인 책은 삭제할수 없습니다.");
                return;
            }
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");
            string deleteQuery = "DELETE FROM books WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(deleteQuery, connection);
            cmd.Parameters.AddWithValue("@id", id);


            try
            {
                connection.Open();

                cmd.ExecuteNonQuery();
                bookDataGridViewConnect(dataGridView);

            } catch(Exception exception)
            {
                MessageBox.Show("선택된 책을 삭제하는데 실패.");
            } finally
            {
                connection.Close();
            }
        }

        public List<Models.Book> getDelayedBooks()
        {
            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none;CharSet=UTF8");
            string selectDelayedBooksQuery = "SELECT * FROM books WHERE returnedAt < CURDATE();";
            MySqlCommand getDelayedBooksCmd = new MySqlCommand(selectDelayedBooksQuery, connection);

            List<Models.Book> delayedBooks = new List<Models.Book>();

            try
            {
                connection.Open();

                MySqlDataReader rd = getDelayedBooksCmd.ExecuteReader();

                while (rd.Read())
                {
                    Models.Book delayedBook = new Models.Book();
                    delayedBook.id = int.Parse(rd["id"].ToString());
                    delayedBook.isbn = rd["isbn"].ToString();
                    delayedBook.name = rd["name"].ToString();
                    delayedBook.publisher = rd["publisher"].ToString();
                    delayedBook.page = int.Parse(rd["page"].ToString());
                    delayedBook.userId = int.Parse(rd["userId"].ToString());
                    delayedBook.userName = rd["userName"].ToString();
                    delayedBook.isBorrowed = int.Parse(rd["isBorrowed"].ToString());
                    delayedBook.borrowedAt = Convert.ToDateTime(rd["borrowedAt"]);
                    delayedBook.returnedAt = Convert.ToDateTime(rd["returnedAt"]);
                    delayedBooks.Add(delayedBook);
                }




                
            }
            catch (Exception exception)
            {
                MessageBox.Show("연체목록 갖고오기 실패.");
            }
            finally
            {
                connection.Close();
            }

            return delayedBooks;
        }


    }
}
