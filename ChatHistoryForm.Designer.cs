namespace TCPGameChatProject
{
    partial class ChatHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private ListBox listBoxHistory;
        private Button btnExport;
        private Button btnImport;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            listBoxHistory = new ListBox { Location = new System.Drawing.Point(12, 12), Size = new System.Drawing.Size(360, 300) };
            btnExport = new Button { Location = new System.Drawing.Point(12, 330), Text = "匯出" };
            btnImport = new Button { Location = new System.Drawing.Point(100, 330), Text = "匯入" };

            btnExport.Click += btnExport_Click;
            btnImport.Click += btnImport_Click;

            ClientSize = new System.Drawing.Size(400, 380);
            Controls.AddRange(new Control[] { listBoxHistory, btnExport, btnImport });
            Text = "聊天紀錄";
        }
    }
}
