using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TCPGameChatProject
{
    public static class PacketHelper
    {
        // 封裝傳送封包
        public static void SendPacket(NetworkStream stream, string message)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(message);
            byte[] lengthBytes = BitConverter.GetBytes(dataBytes.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(lengthBytes);

            stream.Write(lengthBytes, 0, 4);
            stream.Write(dataBytes, 0, dataBytes.Length);
        }

        // 讀取完整封包
        public static string ReceivePacket(NetworkStream stream)
        {
            byte[] lengthBytes = ReadExactly(stream, 4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(lengthBytes);
            int packetLength = BitConverter.ToInt32(lengthBytes, 0);

            byte[] dataBytes = ReadExactly(stream, packetLength);
            return Encoding.UTF8.GetString(dataBytes);
        }

        // 核心完整讀取封裝
        private static byte[] ReadExactly(NetworkStream stream, int size)
        {
            byte[] buffer = new byte[size];
            int totalRead = 0;
            while (totalRead < size)
            {
                int read = stream.Read(buffer, totalRead, size - totalRead);
                if (read == 0) throw new IOException("連線中斷");
                totalRead += read;
            }
            return buffer;
        }
    }
}
