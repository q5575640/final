using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Imaging;

namespace TCPGameChatProject
{
    public partial class ClientForm : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;

        private RockPaperScissorsForm rpsForm;
        private DiceGameForm diceForm;
        private ChatHistoryForm historyForm;

        public ClientForm()
        {
            InitializeComponent();
            rpsForm = new RockPaperScissorsForm(this);
            diceForm = new DiceGameForm(this);
            historyForm = new ChatHistoryForm();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient();
                client.Connect(txtIP.Text, int.Parse(txtPort.Text));
                stream = client.GetStream();

                receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();

                AppendMessage("✅ 已連線到伺服器");
            }
            catch (Exception ex)
            {
                AppendMessage("❌ 連線失敗: " + ex.Message);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            stream?.Close();
            client?.Close();
            receiveThread?.Abort();
            AppendMessage("⚠️ 已中斷連線");
        }

        private void ReceiveMessages()
        {
            try
            {
                while (true)
                {
                    string message = PacketHelper.ReceivePacket(stream);

                    if (message.StartsWith("[TEXT]:"))
                    {
                        string textMsg = message.Substring(7);
                        AppendMessage("對方: " + textMsg);
                        historyForm.AddMessage("對方: " + textMsg);
                    }
                    else if (message.StartsWith("[IMAGE]:"))
                    {
                        string base64Str = message.Substring(8);
                        ShowImageFromBase64(base64Str);
                        historyForm.AddMessage("對方傳送了一張圖片");
                    }
                    else if (message.StartsWith("[RESULT]:"))
                    {
                        AppendMessage("🎮 遊戲結果：" + message.Substring(9));
                        historyForm.AddMessage("🎮 遊戲結果：" + message.Substring(9));

                        // 重置遊戲回合控制
                        if (message.Contains("RPS|"))
                            rpsForm.ResetButtons();
                        else if (message.Contains("DICE|"))
                            diceForm.ResetButton();
                    }
                    else
                    {
                        AppendMessage("📥 收到：" + message);
                    }
                }
            }
            catch
            {
                AppendMessage("⚠️ 連線已關閉");
            }
        }

        private void ShowImageFromBase64(string base64Str)
        {
            panelChat.Controls.Clear();
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64Str);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image img = Image.FromStream(ms);
                    Invoke(new Action(() =>
                    {
                        PictureBox pictureBox = new PictureBox
                        {
                            Image = img,
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Width = 200,
                            Height = 200
                        };
                        panelChat.Controls.Add(pictureBox);
                    }));
                }
            }
            catch
            {
                AppendMessage("⚠️ 圖片解碼失敗");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string text = txtInput.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                SendPacket("[TEXT]:" + text);
                AppendMessage("我: " + text);
                historyForm.AddMessage("我: " + text);
            }
            txtInput.Clear();
        }

        private void btnSendImage_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(ofd.FileName);
                Image resized = ResizeImage(img, 800, 800);

                using (MemoryStream ms = new MemoryStream())
                {
                    var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    var encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 85L);
                    resized.Save(ms, encoder, encoderParams);

                    string imgBase64 = Convert.ToBase64String(ms.ToArray());
                    SendPacket("[IMAGE]:" + imgBase64);
                }
            }
        }

        private Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var resized = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(resized))
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return resized;
        }

        private void btnRPS_Click(object sender, EventArgs e)
        {
            rpsForm.ShowDialog();
        }

        private void btnDice_Click(object sender, EventArgs e)
        {
            diceForm.ShowDialog();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            historyForm.ShowDialog();
        }

        // 新的發送封裝
        public void SendPacket(string message)
        {
            PacketHelper.SendPacket(stream, message);
        }

        private void AppendMessage(string msg)
        {
            Invoke(new Action(() =>
            {
                txtChat.AppendText(msg + Environment.NewLine);
            }));
        }
    }
}
