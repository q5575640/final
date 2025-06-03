using System;
using System.Windows.Forms;

namespace TCPGameChatProject
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DialogResult result = MessageBox.Show("啟動伺服器?", "啟動模式", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Run(new ServerForm());
            }
            else
            {
                Application.Run(new ClientForm());
            }
        }
    }
}