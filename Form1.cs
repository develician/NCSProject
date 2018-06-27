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

namespace BookManager
{
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }


    public partial class Form1 : Form
    {
        public const int WM_COPYDATA = 0x4A;
        
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);


        public Form1()
        {
            InitializeComponent();
            Text = "도서관 관리";

            DBUtils.DBUser dBUser = new DBUtils.DBUser();
            DBUtils.DBBook dBBook = new DBUtils.DBBook();

            // 라벨 설정
            //label5.Text = DataManager.Books.Count.ToString();
            label5.Text = dBBook.getBookCount().ToString();
            label6.Text = DataManager.Users.Count.ToString();
            label7.Text = DataManager.Books.Where((x) => x.isBorrowed).Count().ToString();
            label8.Text = DataManager.Books.Where((x) =>
            {
                return x.isBorrowed && x.BorrowedAt.AddDays(7) > DateTime.Now;
            }).Count().ToString();

            // 데이터 그리드 설정
            //dataGridView1.DataSource = DataManager.Books;
            dBBook.bookDataGridViewConnect(dataGridView1);
            dataGridView1.ReadOnly = true;

            //dataGridView2.DataSource = DataManager.Users;
            dBUser.userDataGridViewConnect(dataGridView2);
            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
            dataGridView2.CurrentCellChanged += DataGridView2_CurrentCellChanged;

            // 버튼 이벤트 설정
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;

            // 대여 및 반납기록 클릭 이벤트
            대여및반납기록ToolStripMenuItem.Click += ShowHistoryForm;
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            

            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                string bookIsbn = selectedRow.Cells["isbn"].Value.ToString();
                string bookName = selectedRow.Cells["name"].Value.ToString();
                string userId = selectedRow.Cells["userId"].Value.ToString();

                textBox1.Text = bookIsbn;
                textBox2.Text = bookName;
                textBox3.Text = userId;
               
            }





            //try
            //{



            //    // 그리드의 셀이 선택되면 텍스트박스에 글자 지정
            //    Book book = dataGridView1.CurrentRow.DataBoundItem as Book;
            //    textBox1.Text = book.Isbn;
            //    textBox2.Text = book.Name;
            //}
            //catch (Exception exception)
            //{

            //}
        }

        private void DataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {

            if (dataGridView2.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView2.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView2.Rows[selectedRowIndex];

                string userId = selectedRow.Cells["id"].Value.ToString();

                textBox3.Text = userId;

            }


            //try
            //{
            //    // 그리드의 셀이 선택되면 텍스트박스에 글자 지정
            //    User book = dataGridView2.CurrentRow.DataBoundItem as User;
            //    textBox3.Text = book.Id.ToString();
            //}
            //catch (Exception exception)
            //{

            //}
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Isbn을 입력해주세요");
            }
            else if (textBox3.Text.Trim() == "")
            {
                MessageBox.Show("사용자 Id를 입력해주세요");
            }
            else
            {
                try
                {
                    Book book = DataManager.Books.Single((x) => x.Isbn == textBox1.Text);
                    if (book.isBorrowed)
                    {
                        MessageBox.Show("이미 대여 중인 도서입니다.");
                    }
                    else
                    {
                        User user = DataManager.Users.Single((x) => x.Id.ToString() == textBox3.Text);
                        book.UserId = user.Id;
                        book.UserName = user.Name;
                        book.isBorrowed = true;
                        book.BorrowedAt = DateTime.Now;

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Books;
                        DataManager.Save();

                        MessageBox.Show("\"" + book.Name + "\"이/가 \"" + user.Name + "\"님께 대여되었습니다.");
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("존재하지 않는 도서 또는 사용자입니다.");
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Isbn을 입력해주세요");
            }
            else
            {
                try
                {
                    Book book = DataManager.Books.Single((x) => x.Isbn == textBox1.Text);
                    if (book.isBorrowed)
                    {
                        User user = DataManager.Users.Single((x) => x.Id.ToString() == book.UserId.ToString());
                        book.UserId = 0;
                        book.UserName = "";
                        book.isBorrowed = false;
                        book.BorrowedAt = new DateTime();

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Books;
                        DataManager.Save();

                        if (book.BorrowedAt.AddDays(7) > DateTime.Now)
                        {
                            MessageBox.Show("\"" + book.Name + "\"이/가 연체 상태로 반납되었습니다.");
                        }
                        else
                        {
                            MessageBox.Show("\"" + book.Name + "\"이/가 반납되었습니다.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("대여 상태가 아닙니다.");
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("존재하지 않는 도서 또는 사용자입니다.");
                }
            }
        }

        private void 도서관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }

        private void 사용자관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog();
        }

        private void ShowHistoryForm(object sender, EventArgs e)
        {
            HistoryForm historyForm = new HistoryForm();
            historyForm.Show();
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
                        label5.Text = dBBook.getBookCount().ToString();
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
