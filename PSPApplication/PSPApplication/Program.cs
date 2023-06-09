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
            var server = new FileSharingServer();
            server.Start();
        }
    }
    class FileSharingServer
    {
        TcpListener listener;

        public void Start()
        {
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
            listener.Start();

            Console.WriteLine("Waiting to receive files...");

            var clientThread = new Thread(AcceptConnections);
            clientThread.Start();
        }

        public void AcceptConnections()
        {
            while(true)
            {
                TcpClient client = listener.AcceptTcpClient();

                var stream = client.GetStream();
                byte[] fileData = new byte[1024];

                stream.Read(fileData);

                int receivedBytes = stream.Read(fileData, 0, fileData.Length);
                using (var fs = File.Create("receivedFile.txt"))
                {
                    fs.Write(fileData, 0, receivedBytes);
                }
                Console.WriteLine("File received from the Client");
            }
        }
    }
}
