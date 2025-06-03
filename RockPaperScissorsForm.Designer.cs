namespace TCPGameChatProject
{
    partial class RockPaperScissorsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnRock, btnPaper, btnScissors;
        private Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnRock = new Button { Location = new System.Drawing.Point(30, 20), Text = "石頭" };
            btnPaper = new Button { Location = new System.Drawing.Point(110, 20), Text = "布" };
            btnScissors = new Button { Location = new System.Drawing.Point(190, 20), Text = "剪刀" };
            lblStatus = new Label { Location = new System.Drawing.Point(30, 70), Width = 300, Text = "請出拳" };

            btnRock.Click += btnRock_Click;
            btnPaper.Click += btnPaper_Click;
            btnScissors.Click += btnScissors_Click;

            ClientSize = new System.Drawing.Size(300, 120);
            Controls.AddRange(new Control[] { btnRock, btnPaper, btnScissors, lblStatus });
            Text = "猜拳";
        }
    }
}
