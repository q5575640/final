namespace TCPGameChatProject
{
    partial class DiceGameForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnRoll;
        private Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnRoll = new Button { Location = new System.Drawing.Point(30, 20), Text = "擲骰" };
            lblStatus = new Label { Location = new System.Drawing.Point(30, 70), Width = 300, Text = "請擲骰" };

            btnRoll.Click += btnRoll_Click;

            ClientSize = new System.Drawing.Size(300, 120);
            Controls.AddRange(new Control[] { btnRoll, lblStatus });
            Text = "比大小";
        }
    }
}
