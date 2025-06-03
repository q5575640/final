using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace TCPGameChatProject
{
    public partial class ServerForm : Form
    {
        private TcpListener server;
        private List<TcpClient> clients = new List<TcpClient>();
        private Thread listenThread;

        private Dictionary<TcpClient, string> rpsChoices = new Dictionary<TcpClient, string>();
        private Dictionary<TcpClient, int> diceChoices = new Dictionary<TcpClient, int>();

        public ServerForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            server = new TcpListener(IPAddress.Any, 8888);
            server.Start();
            listenThread = new Thread(ListenClients);
            listenThread.IsBackground = true;
            listenThread.Start();
            AppendLog("✅ 伺服器已啟動");
        }

        private void ListenClients()
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client);
                AppendLog("新用戶已連線");
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.IsBackground = true;
                clientThread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            try
            {
                while (true)
                {
                    string message = PacketHelper.ReceivePacket(stream);

                    if (message.StartsWith("[IMAGE]:"))
                        AppendLog("收到圖片訊息");
                    else
                        AppendLog("收到：" + message);

                    if (message.StartsWith("[TEXT]:"))
                        Broadcast(message, client);
                    else if (message.StartsWith("[IMAGE]:"))
                        Broadcast(message, client);
                    else if (message.StartsWith("[RPS]:"))
                    {
                        string choice = message.Substring(6);
                        rpsChoices[client] = choice;
                        CheckRPS();
                    }
                    else if (message.StartsWith("[DICE]:"))
                    {
                        int dice = int.Parse(message.Substring(7));
                        diceChoices[client] = dice;
                        CheckDice();
                    }
                }
            }
            catch
            {
                clients.Remove(client);
                AppendLog("用戶中斷連線");
            }
        }

        private void CheckRPS()
        {
            if (rpsChoices.Count >= 2)
            {
                var enumerator = rpsChoices.GetEnumerator();
                enumerator.MoveNext();
                var p1 = enumerator.Current;
                enumerator.MoveNext();
                var p2 = enumerator.Current;

                string result1 = JudgeRPS(p1.Value, p2.Value);
                string result2 = ReverseResult(result1);

                SendResult(p1.Key, $"[RESULT]:RPS|你出:{p1.Value}|對方出:{p2.Value}|{result1}");
                SendResult(p2.Key, $"[RESULT]:RPS|你出:{p2.Value}|對方出:{p1.Value}|{result2}");

                rpsChoices.Clear();
            }
        }

        private void CheckDice()
        {
            if (diceChoices.Count >= 2)
            {
                var enumerator = diceChoices.GetEnumerator();
                enumerator.MoveNext();
                var p1 = enumerator.Current;
                enumerator.MoveNext();
                var p2 = enumerator.Current;

                string result1 = JudgeDice(p1.Value, p2.Value);
                string result2 = ReverseResult(result1);

                SendResult(p1.Key, $"[RESULT]:DICE|你出:{p1.Value}|對方出:{p2.Value}|{result1}");
                SendResult(p2.Key, $"[RESULT]:DICE|你出:{p2.Value}|對方出:{p1.Value}|{result2}");

                diceChoices.Clear();
            }
        }

        private string JudgeRPS(string a, string b)
        {
            if (a == b) return "平手";
            if ((a == "ROCK" && b == "SCISSORS") || (a == "PAPER" && b == "ROCK") || (a == "SCISSORS" && b == "PAPER"))
                return "你贏了";
            return "你輸了";
        }

        private string JudgeDice(int a, int b)
        {
            if (a == b) return "平手";
            if (a > b) return "你贏了";
            return "你輸了";
        }

        private string ReverseResult(string result)
        {
            if (result == "你贏了") return "你輸了";
            if (result == "你輸了") return "你贏了";
            return "平手";
        }

        private void SendResult(TcpClient client, string result)
        {
            try
            {
                PacketHelper.SendPacket(client.GetStream(), result);
            }
            catch { }
        }

        private void Broadcast(string message, TcpClient exclude)
        {
            foreach (TcpClient c in clients)
            {
                if (c != exclude)
                {
                    try
                    {
                        PacketHelper.SendPacket(c.GetStream(), message);
                    }
                    catch { }
                }
            }
        }

        private void AppendLog(string msg)
        {
            if (InvokeRequired)
                Invoke(new Action(() => txtLog.AppendText(msg + Environment.NewLine)));
            else
                txtLog.AppendText(msg + Environment.NewLine);
        }
    }
}
