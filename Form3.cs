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
    public partial class Form3 : Form
    {

        public const int WM_USERDATA = 0x4B;

        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        int id;
        string name;
        string phone;

        public Form3()
        {



            InitializeComponent();
            Text = "사용자 관리";

            // 데이터 그리드 설정
            //dataGridView1.DataSource = DataManager.Users;
            DBUtils.DBUser dBUser = new DBUtils.DBUser();
            dBUser.userDataGridViewConnect(dataGridView1);
            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
            dataGridView1.ReadOnly = true;

            // 버튼 설정
            button1.Click += insertNewUser;
            //button1.Click += (sender, e) =>
            //{
            //// 추가 버튼
            //try
            //    {
            //        if (DataManager.Users.Exists((x) => x.Id == int.Parse(textBox1.Text)))
            //        {
            //            MessageBox.Show("사용자 ID가 겹칩니다");
            //        }
            //        else
            //        {
            //            User user = new User()
            //            {
            //                Id = int.Parse(textBox1.Text),
            //                Name = textBox2.Text
            //            };
            //            DataManager.Users.Add(user);

            //            dataGridView1.DataSource = null;
            //            dataGridView1.DataSource = DataManager.Users;
            //            DataManager.Save();
            //        }
            //    }
            //    catch (Exception exception)
            //    {

            //    }

            //};

            //button2.Click += (sender, e) =>
            //{
            //// 수정 버튼
            //try
            //    {
            //        User user = DataManager.Users.Single((x) => x.Id == int.Parse(textBox1.Text));
            //        user.Name = textBox2.Text;

            //        dataGridView1.DataSource = null;
            //        dataGridView1.DataSource = DataManager.Users;
            //    }
            //    catch (Exception exception)
            //    {
            //        MessageBox.Show("존재하지 않는 사용자입니다");
            //    }
            //};
            button2.Click += updateButtonClick;

            button3.Click += removeSelectedUser;
            //button3.Click += (sender, e) =>
            //{
            //// 수정 버튼
            //try
            //    {
            //        User user = DataManager.Users.Single((x) => x.Id == int.Parse(textBox1.Text));
            //        DataManager.Users.Remove(user);

            //        dataGridView1.DataSource = null;
            //        dataGridView1.DataSource = DataManager.Users;
            //        DataManager.Save();
            //    }
            //    catch (Exception exception)
            //    {
            //        MessageBox.Show("존재하지 않는 사용자입니다");
            //    }
            //};

            userSearchCombo.Items.Add("이름");
            userSearchCombo.Items.Add("휴대폰 번호");
            userSearchCombo.SelectedIndex = 0;

            searchButton.Click += searchButtonClick;
            searchAllButton.Click += searchAll;
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                id = int.Parse(selectedRow.Cells["id"].Value.ToString());
                name = selectedRow.Cells["name"].Value.ToString();
                phone = selectedRow.Cells["phone"].Value.ToString();

                textBox1.Text = name;
                textBox2.Text = phone;

            }






            //try
            //{
            //    // 그리드의 셀이 선택되면 텍스트박스에 글자 지정
            //    User user = dataGridView1.CurrentRow.DataBoundItem as User;
            //    textBox1.Text = user.Id.ToString();
            //    textBox2.Text = user.Name;
            //}
            //catch (Exception exception)
            //{

            //}
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void insertNewUser(object sender, EventArgs e)
        {
            DBUtils.DBUser dBUser = new DBUtils.DBUser();
            string name = textBox1.Text.ToString();
            string phone = textBox2.Text.ToString();

            dBUser.insertUser(dataGridView1, name, phone);
            sendInsertUserSuccessMessage();

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void removeSelectedUser(object sender, EventArgs e)
        {
            DBUtils.DBUser dBUser = new DBUtils.DBUser();
            dBUser.removeSelectedUser(dataGridView1, id);
            sendRemoveUserSuccessMessage();
        }

        private void updateButtonClick(object sender, EventArgs e)
        {
            UpdateUserForm updateUserForm = new UpdateUserForm(id, name, phone);
            updateUserForm.ShowDialog();
        }

        private void searchButtonClick(object sender, EventArgs e)
        {
            DBUtils.DBUser dBUser = new DBUtils.DBUser();
            string searchWord = searchWordTextBox.Text.ToString();
            int comboIndex = userSearchCombo.SelectedIndex;
            List<Models.User> searchedUserList = dBUser.searchUser(comboIndex, searchWord);
            var bindingSearchedList = new BindingList<Models.User>(searchedUserList);
            dataGridView1.DataSource = bindingSearchedList;
        }

        private void searchAll(object sender, EventArgs e)
        {
            DBUtils.DBUser dBUser = new DBUtils.DBUser();
            dBUser.userDataGridViewConnect(dataGridView1);
        }

        private void sendInsertUserSuccessMessage()
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
            SendMessage(hwnd, WM_USERDATA, IntPtr.Zero, ref cds);
        }

        private void sendRemoveUserSuccessMessage()
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
            SendMessage(hwnd, WM_USERDATA, IntPtr.Zero, ref cds);
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                switch (m.Msg)
                {
                    case WM_USERDATA:
                        DBUtils.DBUser dBUser = new DBUtils.DBUser();
                        dBUser.userDataGridViewConnect(dataGridView1);
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
