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
    public partial class UpdateUserForm : Form
    {
        public const int WM_USERDATA = 0x4B;

        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        int userId;
        public UpdateUserForm(int userId, string userName, string phone)
        {
            InitializeComponent();

            this.userId = userId;

            userNameTextBox.Text = userName;
            phoneTextBox.Text = phone;

            updateButton.Click += updateUser;
        }

        private void updateUser(object sender, EventArgs e)
        {
            DBUtils.DBUser dBUser = new DBUtils.DBUser();
            dBUser.updateUserInfo(userId, userNameTextBox.Text.ToString(), phoneTextBox.Text.ToString());
            sendUpdateUserSuccessMessage();
            this.Close();
        }

        private void sendUpdateUserSuccessMessage()
        {
            //string msg = this.tbMsg.Text.Trim();
            //if (string.IsNullOrEmpty(msg))
            //{
            //    MessageBox.Show("메세지를 입력해주세요");
            //    return;
            //}
            IntPtr hwnd = FindWindow(null, "도서관 관리");
            IntPtr hwnd2 = FindWindow(null, "사용자 관리");
            //byte[] buff = System.Text.Encoding.Default.GetBytes(msg);
            COPYDATASTRUCT cds = new COPYDATASTRUCT();
            //cds.dwData = new IntPtr(1001);
            //cds.cbData = buff.Length + 1;
            //cds.lpData = msg;
            SendMessage(hwnd, WM_USERDATA, IntPtr.Zero, ref cds);
            SendMessage(hwnd2, WM_USERDATA, IntPtr.Zero, ref cds);


        }


    }
}
