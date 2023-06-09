using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace P2PApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new FileSharingClient();
            client.Connect("127.0.0.1", 8000);

            client.SendFile("sampleFile.txt");
            client.Disconnect();
        }
    }

    class FileSharingClient
    {
        TcpClient client;

        public void Connect(string serverIP, int serverPort)
        {
            client = new TcpClient();
            client.Connect(serverIP, serverPort);

            Console.WriteLine("Connected to the Server");
        }

        public void SendFile(string path)
        {
            var fileData = File.ReadAllBytes(path);
            var stream = client.GetStream();

            stream.Write(fileData, 0 , fileData.Length);
            Console.WriteLine("File sent to the server");

            stream.Close();
        }

        public void Disconnect()
        {
            client.Close();
            Console.WriteLine("Disconnected by client");
        }
    }
}
