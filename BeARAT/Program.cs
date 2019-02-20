using System;
using System.Net;

namespace BeARAT.Server
{
    class Program
    {
        private const string SERVER_NAME = "Server";
        private const string IP = "127.0.0.1";
        private const int PORT = 8080;

        public static void Main(String[] args)
        {
            IPAddress ipAddr = IPAddress.Parse(IP);
            Listener server = new Listener(SERVER_NAME, ipAddr, PORT);

            server.Start();
        }
    }
}
