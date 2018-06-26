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
    public partial class HistoryForm : Form
    {
        public HistoryForm()
        {
            InitializeComponent();

            DBHistory dbHistory = new DBHistory();
            dbHistory.historyGridViewConnect(historyGridView);

            historyGridView.ReadOnly = true;
            historyGridView.Columns[0].HeaderText = "사용자 번호";
            historyGridView.Columns[1].HeaderText = "사용자 이름";
            historyGridView.Columns[2].HeaderText = "빌린 날짜";
            historyGridView.Columns[3].HeaderText = "반납 날짜";
            historyGridView.Columns[4].HeaderText = "책 이름";
            historyGridView.Columns[5].HeaderText = "책 번호";

            removeButton.Click += removeButtonClick;
        }

        private void removeButtonClick(object sender, EventArgs e)
        {
            DBHistory dbHistory = new DBHistory();
            dbHistory.removeAllHistory(historyGridView);
        }

     
    }
}
