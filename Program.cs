using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManager
{
    static class Program
    {
        public static ApplicationContext ac = new ApplicationContext();
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LoginForm loginForm = new LoginForm();
            ac.MainForm = loginForm;
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(ac);
        }
    }
}
