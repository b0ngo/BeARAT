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
                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", 8080);
                Common.IO.Console.Message("Connected");

                string data = "Testdata with some spaces...";

                ASCIIEncoding asen = new ASCIIEncoding();
                StreamWriter writer = new StreamWriter(client.GetStream());
                writer.WriteLine(data);
                writer.Flush();

            } catch (Exception e) {
                Common.IO.Console.Error(e);
            }
        }
    }
}
