namespace TCPGameChatProject
{
    partial class ClientForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtIP, txtPort, txtInput, txtChat;
        private Panel panelChat;
        private Button btnConnect, btnDisconnect, btnSend, btnSendImage;
        private Button btnRPS, btnDice, btnHistory;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtIP = new TextBox { Location = new System.Drawing.Point(12, 12), Text = "127.0.0.1" };
            txtPort = new TextBox { Location = new System.Drawing.Point(130, 12), Text = "8888" };

            btnConnect = new Button { Location = new System.Drawing.Point(250, 10), Text = "連線" };
            btnDisconnect = new Button { Location = new System.Drawing.Point(330, 10), Text = "斷線" };

            txtChat = new TextBox { Location = new System.Drawing.Point(12, 50), Width = 500, Height = 200, Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true };
            panelChat = new Panel { Location = new System.Drawing.Point(12, 260), Width = 500, Height = 150, AutoScroll = true, BorderStyle = BorderStyle.FixedSingle };

            txtInput = new TextBox { Location = new System.Drawing.Point(12, 420), Width = 400 };
            btnSend = new Button { Location = new System.Drawing.Point(420, 418), Text = "發送文字" };

            btnSendImage = new Button { Location = new System.Drawing.Point(12, 460), Text = "發送圖片" };
            btnRPS = new Button { Location = new System.Drawing.Point(120, 460), Text = "猜拳" };
            btnDice = new Button { Location = new System.Drawing.Point(220, 460), Text = "比大小" };
            btnHistory = new Button { Location = new System.Drawing.Point(320, 460), Text = "紀錄" };

            btnConnect.Click += btnConnect_Click;
            btnDisconnect.Click += btnDisconnect_Click;
            btnSend.Click += btnSend_Click;
            btnSendImage.Click += btnSendImage_Click;
            btnRPS.Click += btnRPS_Click;
            btnDice.Click += btnDice_Click;
            btnHistory.Click += btnHistory_Click;

            ClientSize = new System.Drawing.Size(530, 520);
            Controls.AddRange(new Control[] { txtIP, txtPort, btnConnect, btnDisconnect, txtChat, panelChat, txtInput, btnSend, btnSendImage, btnRPS, btnDice, btnHistory });
            Text = "客戶端";
        }
    }
}
