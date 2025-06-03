using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace TCPGameChatProject
{
    public partial class ChatHistoryForm : Form
    {
        private List<string> messageHistory = new List<string>();

        public ChatHistoryForm()
        {
            InitializeComponent();
        }

        public void AddMessage(string message)
        {
            messageHistory.Add(message);
            listBoxHistory.Items.Add(message);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文字檔 (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog.FileName, messageHistory);
                MessageBox.Show("匯出完成！");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文字檔 (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                messageHistory.Clear();
                listBoxHistory.Items.Clear();
                var lines = File.ReadAllLines(openFileDialog.FileName);
                foreach (var line in lines)
                {
                    messageHistory.Add(line);
                    listBoxHistory.Items.Add(line);
                }
                MessageBox.Show("匯入完成！");
            }
        }
    }
}
