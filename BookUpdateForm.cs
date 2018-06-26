using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManager
{
    public partial class BookUpdateForm : Form
    {


        public BookUpdateForm(int id, string isbn, string bookName, string publisher, int page)
        {
            InitializeComponent();

            isbnTextBox.Text = isbn;
            bookNameTextBox.Text = bookName;
            publisherTextBox.Text = publisher;
            pageTextBox.Text = page.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
