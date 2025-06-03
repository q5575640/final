using System;
using System.Windows.Forms;

namespace TCPGameChatProject
{
    public partial class RockPaperScissorsForm : Form
    {
        private ClientForm parent;

        public RockPaperScissorsForm(ClientForm clientForm)
        {
            InitializeComponent();
            parent = clientForm;
        }

        private void btnRock_Click(object sender, EventArgs e)
        {
            parent.SendPacket("[RPS]:ROCK");
            lblStatus.Text = "你出：石頭，等待對手...";
            DisableButtons();
        }

        private void btnPaper_Click(object sender, EventArgs e)
        {
            parent.SendPacket("[RPS]:PAPER");
            lblStatus.Text = "你出：布，等待對手...";
            DisableButtons();
        }

        private void btnScissors_Click(object sender, EventArgs e)
        {
            parent.SendPacket("[RPS]:SCISSORS");
            lblStatus.Text = "你出：剪刀，等待對手...";
            DisableButtons();
        }

        private void DisableButtons()
        {
            btnRock.Enabled = false;
            btnPaper.Enabled = false;
            btnScissors.Enabled = false;
        }

        // 提供 ClientForm 觸發：每次比完後讓按鈕重置
        public void ResetButtons()
        {
            btnRock.Enabled = true;
            btnPaper.Enabled = true;
            btnScissors.Enabled = true;
            lblStatus.Text = "請出拳";
        }
    }
}
