namespace TCPGameChatProject
{
    partial class ServerForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnStart;
        private TextBox txtLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnStart = new Button { Location = new System.Drawing.Point(12, 12), Text = "啟動伺服器" };
            txtLog = new TextBox { Location = new System.Drawing.Point(12, 50), Width = 500, Height = 300, Multiline = true, ScrollBars = ScrollBars.Vertical };

            btnStart.Click += btnStart_Click;

            ClientSize = new System.Drawing.Size(530, 370);
            Controls.AddRange(new Control[] { btnStart, txtLog });
            Text = "伺服器";
        }
    }
}
