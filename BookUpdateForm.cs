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
    public partial class BookUpdateForm : Form
    {
        public const int WM_COPYDATA = 0x4A;
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);


        int id;
        string isbn;
        string bookName;
        string publisher;
        int page;

        public BookUpdateForm(int id, string isbn, string bookName, string publisher, int page)
        {
            InitializeComponent();
            this.id = id;
            this.isbn = isbn;
            this.bookName = bookName;
            this.publisher = publisher;
            this.page = page;

            isbnTextBox.Text = isbn;
            bookNameTextBox.Text = bookName;
            publisherTextBox.Text = publisher;
            pageTextBox.Text = page.ToString();

            updateButton.Click += updateButtonClick;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void updateButtonClick(object sender, EventArgs e)
        {
            DBUtils.DBBook dBBook = new DBUtils.DBBook();
            string isbn = isbnTextBox.Text.ToString();
            string bookName = bookNameTextBox.Text.ToString();
            string publisher = publisherTextBox.Text.ToString();
            int page = int.Parse(pageTextBox.Text.ToString());

            dBBook.updateBookInfo(id, isbn, bookName, publisher, page);
            sendUpdateSuccessMessage();
            this.Close();

        }

        private void sendUpdateSuccessMessage()
        {
            //string msg = this.tbMsg.Text.Trim();
            //if (string.IsNullOrEmpty(msg))
            //{
            //    MessageBox.Show("메세지를 입력해주세요");
            //    return;
            //}
            IntPtr hwnd = FindWindow(null, "도서 관리");
            //byte[] buff = System.Text.Encoding.Default.GetBytes(msg);
            COPYDATASTRUCT cds = new COPYDATASTRUCT();
            //cds.dwData = new IntPtr(1001);
            //cds.cbData = buff.Length + 1;
            //cds.lpData = msg;
            SendMessage(hwnd, WM_COPYDATA, IntPtr.Zero, ref cds);


        }
    }
}
