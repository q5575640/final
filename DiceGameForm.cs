using System;
using System.Windows.Forms;

namespace TCPGameChatProject
{
    public partial class DiceGameForm : Form
    {
        private ClientForm parent;
        private Random random = new Random();

        public DiceGameForm(ClientForm clientForm)
        {
            InitializeComponent();
            parent = clientForm;
        }

        private void btnRoll_Click(object sender, EventArgs e)
        {
            int dice = random.Next(1, 7);
            parent.SendPacket("[DICE]:" + dice);
            lblStatus.Text = $"你擲出了：{dice}，等待對手...";
            btnRoll.Enabled = false;
        }

        // 提供 ClientForm 觸發：每次比完後讓按鈕重置
        public void ResetButton()
        {
            btnRoll.Enabled = true;
            lblStatus.Text = "請擲骰";
        }
    }
}
