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
    public partial class UserInfoForm : Form
    {
        int id;
        public UserInfoForm(int id)
        {
            InitializeComponent();

            this.id = id;

            userNameTextBox.Enabled = false;
            userPhoneTextBox.Enabled = false;

            getUserInfo(id);
        }

        private void getUserInfo(int id)
        {
            DBUtils.DBUser dBUser = new DBUtils.DBUser();
            Models.User user = dBUser.getUserInfo(id);

            //MessageBox.Show(user.name);

            userNameTextBox.Text = user.name;
            userPhoneTextBox.Text = user.phone;
        }
    }
}
