using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace BookManager
{
    //public struct COPYDATASTRUCT
    //{
    //    public IntPtr dwData;
    //    public int cbData;
    //    [MarshalAs(UnmanagedType.LPStr)]
    //    public string lpData;
    //}


    public partial class Form2 : Form
    {

        public const int WM_COPYDATA = 0x4A;
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        public static int selectedId;
        public static string selectedIsbn;
        public static string selectedBookName;
        public static string selectedPublisher;
        public static int selectedPage;

        public Form2()
        {
            InitializeComponent();
            Text = "도서 관리";



            DBUtils.DBBook dBBook = new DBUtils.DBBook();

            // 데이터 그리드 설정
            //dataGridView1.DataSource = DataManager.Books;
            dBBook.bookDataGridViewConnect(dataGridView1);
            dataGridView1.ReadOnly = true;
            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;

            // 버튼 설정
            button1.Click += insertNewBook;
            //button1.Click += messageTesting;
            //button1.Click += (sender, e) =>
            //{
            //// 추가 버튼
            //try
            //    {
            //        if (DataManager.Books.Exists((x) => x.Isbn == textBox1.Text))
            //        {
            //            MessageBox.Show("이미 존재하는 도서입니다");
            //        }
            //        else
            //        {
            //            Book book = new Book()
            //            {
            //                Isbn = textBox1.Text,
            //                Name = textBox2.Text,
            //                Publisher = textBox3.Text,
            //                Page = int.Parse(textBox4.Text)
            //            };
            //            DataManager.Books.Add(book);

            //            dataGridView1.DataSource = null;
            //            dataGridView1.DataSource = DataManager.Books;
            //            DataManager.Save();
            //        }
            //    }
            //    catch (Exception exception)
            //    {

            //    }
            //};

            button2.Click += updateButtonClick;
            //    += (sender, e) =>
            //{


            //    //BookUpdateForm updateForm = new BookUpdateForm();
            //    //updateForm.ShowDialog();

            //    // 수정 버튼
            //    //try
            //    //{
            //    //    Book book = DataManager.Books.Single((x) => x.Isbn == textBox1.Text);
            //    //    book.Name = textBox2.Text;
            //    //    book.Publisher = textBox3.Text;
            //    //    book.Page = int.Parse(textBox4.Text);

            //    //    dataGridView1.DataSource = null;
            //    //    dataGridView1.DataSource = DataManager.Books;
            //    //    DataManager.Save();
            //    //}
            //    //catch (Exception exception)
            //    //{
            //    //    MessageBox.Show("존재하지 않는 도서입니다");
            //    //}
            //};

            button3.Click += removeSelectedBook;

            //button3.Click += (sender, e) =>
            //{
            //    // 수정 버튼
            //    try
            //    {
            //        Book book = DataManager.Books.Single((x) => x.Isbn == textBox1.Text);
            //        DataManager.Books.Remove(book);

            //        dataGridView1.DataSource = null;
            //        dataGridView1.DataSource = DataManager.Books;
            //        DataManager.Save();
            //    }
            //    catch (Exception exception)
            //    {
            //        MessageBox.Show("존재하지 않는 도서입니다");
            //    }
            //};

            searchButton.Click += getSearchedBooks;

            searchCategoryCombo.Items.Add("isbn");
            searchCategoryCombo.Items.Add("책 이름");
            searchCategoryCombo.Items.Add("출판사");
            searchCategoryCombo.SelectedIndex = 0;
            
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                string bookIsbn = selectedRow.Cells["isbn"].Value.ToString();
                string bookName = selectedRow.Cells["name"].Value.ToString();
                string publisher = selectedRow.Cells["publisher"].Value.ToString();
                string page = selectedRow.Cells["page"].Value.ToString();

                textBox1.Text = bookIsbn;
                textBox2.Text = bookName;
                textBox3.Text = publisher;
                textBox4.Text = page;

            }



            //try
            //{
            //    // 그리드의 셀이 선택되면 텍스트박스에 글자 지정
            //    Book book = dataGridView1.CurrentRow.DataBoundItem as Book;
            //    textBox1.Text = book.Isbn;
            //    textBox2.Text = book.Name;
            //    textBox3.Text = book.Publisher;
            //    textBox4.Text = book.Page.ToString();
            //}
            //catch (Exception exception)
            //{

            //}
        }

        private void insertNewBook(object sender, EventArgs e)
        {
            DBUtils.DBBook dBBook = new DBUtils.DBBook();
            string isbn = textBox1.Text.ToString();
            string name = textBox2.Text.ToString();
            string publisher = textBox3.Text.ToString();
            int page;
            bool isPageParsed = int.TryParse(textBox4.Text.ToString(), out page);
            if (!isPageParsed)
            {
                MessageBox.Show("페이지는 숫자만 입력해주세요.");
                return;
            }
            dBBook.insertNewBook(dataGridView1, isbn, name, publisher, page);
            sendInsertSuccessMessage();


        }

        private void sendInsertSuccessMessage()
        {
            //string msg = this.tbMsg.Text.Trim();
            //if (string.IsNullOrEmpty(msg))
            //{
            //    MessageBox.Show("메세지를 입력해주세요");
            //    return;
            //}
            IntPtr hwnd = FindWindow(null, "도서관 관리");
            //byte[] buff = System.Text.Encoding.Default.GetBytes(msg);
            COPYDATASTRUCT cds = new COPYDATASTRUCT();
            //cds.dwData = new IntPtr(1001);
            //cds.cbData = buff.Length + 1;
            //cds.lpData = msg;
            SendMessage(hwnd, WM_COPYDATA, IntPtr.Zero, ref cds);
            

        }

        private void updateButtonClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                selectedId = int.Parse(selectedRow.Cells["id"].Value.ToString());

                MySqlConnection connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");

                try
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM books WHERE id = @id";

                    MySqlCommand cmd = new MySqlCommand(selectQuery, connection);
                    cmd.Parameters.AddWithValue("id", selectedId);

                    MySqlDataReader rd = cmd.ExecuteReader();

                    if(rd.Read())
                    {
                        selectedIsbn = rd["isbn"].ToString();
                        selectedBookName = rd["name"].ToString();
                        selectedPublisher = rd["publisher"].ToString();
                        selectedPage = int.Parse(rd["page"].ToString());

                        BookUpdateForm updateForm = new BookUpdateForm(selectedId, selectedIsbn, selectedBookName, selectedPublisher, selectedPage);
                        updateForm.ShowDialog();
                    }

                } catch(Exception exception)
                {
                    MessageBox.Show("업데이트할 책 정보를 불러오는데 실패했습니다.");
                } finally
                {
                    connection.Close();
                }
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void getSearchedBooks(object sender, EventArgs e)
        {
            DBUtils.DBBook dBBook = new DBUtils.DBBook();
            int searchIndex = searchCategoryCombo.SelectedIndex;
            string searchWord = searchWordTextBox.Text.ToString();
            List<Models.Book> searchedBooks = dBBook.getSearchedBooks(searchIndex, searchWord);
            MessageBox.Show(searchedBooks.Count.ToString());
        }

        private void searchWordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void searchWordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                getSearchedBooks(sender, e);
            }
        }

        public void removeSelectedBook(object sender, EventArgs e)
        {
            DBUtils.DBBook dBBook = new DBUtils.DBBook();

            int id = 0;
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                id = int.Parse(selectedRow.Cells["id"].Value.ToString());
            }



            dBBook.removeSelectedBook(dataGridView1, id);
            sendInsertSuccessMessage();
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                switch (m.Msg)
                {
                    case WM_COPYDATA:
                        DBUtils.DBBook dBBook = new DBUtils.DBBook();
                        dBBook.bookDataGridViewConnect(dataGridView1);
                        sendInsertSuccessMessage();
                        //COPYDATASTRUCT cds = (COPYDATASTRUCT)m.GetLParam(typeof(COPYDATASTRUCT));
                        //byte[] buff = System.Text.Encoding.Default.GetBytes(cds.lpData);

                        //COPYDATASTRUCT cs = new COPYDATASTRUCT();
                        //cs.dwData = new IntPtr(0);
                        //cs.cbData = buff.Length;
                        //cs.lpData = cds.lpData;

                        // 다시 보낼 Form2의 windows 헨들을 가져 온다.
                        //IntPtr hwnd = FindWindow(null, "Form2");
                        //SendMessage(hwnd, WM_COPYDATA, IntPtr.Zero, ref cs);
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }


}
