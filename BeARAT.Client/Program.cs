using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using BeARAT.Common;

namespace BeARAT.Client
{
    class Program
    {
        public static void Main()
        {
            try {
                Common.IO.Console.Message("Connecting...");
                TcpClient client = new TcpClient("localhost", 8080);
                
                //client.Connect("127.0.0.1", 8080);
                Common.IO.Console.Message("Connected");

                string data = "Hello";

                NetworkStream stream = client.GetStream();
                BinaryWriter w = new BinaryWriter(stream);
                w.Write(data);
                w.Flush();
                Common.IO.Console.Message("Send:     " + data);
                string response = new BinaryReader(stream).ReadString();
                Common.IO.Console.Message("Received: " + response);

            } catch (Exception e) {
                Common.IO.Console.Error(e);
            }
        }
    }
}
