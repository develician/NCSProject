using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BookManager
{
    public partial class LoginForm : Form
    {
        private MySqlConnection connection;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adminId = textBox1.Text.ToString();
            string adminPassword = textBox2.Text.ToString();
            string selectQuery = "SELECT * FROM admin WHERE adminId = @adminId";


            connection = new MySqlConnection("server=localhost;user id=root;password=root1234;persistsecurityinfo=True;port=3306;database=lib;SslMode=none");


            try
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(selectQuery, connection);
                cmd.Parameters.AddWithValue("@adminId", adminId);
                MySqlDataReader rd = cmd.ExecuteReader();

                
                if (rd.Read())
                {
                    if (rd["adminPassword"].ToString() != adminPassword)
                    {
                        MessageBox.Show("비밀번호가 다릅니다.");
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("없는 아이디 입니다.");
                    return;
                }


                Form1 form1 = new Form1();
                ////form1.Show();
                ////this.Visible = false;

                Program.ac.MainForm = form1;
                form1.Show();
                this.Close();

            }
            catch (Exception exception)
            {
                MessageBox.Show("연결 오류");
            }
            finally
            {
                connection.Close();
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                button1_Click(sender, e);
            }
        }
    }
}
