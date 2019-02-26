using BeARAT.Common.IO;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace BeARAT.Server
{
    class Program
    {
        private const string SERVER_NAME = "Server";
        private const string IP = "127.0.0.1";
        private const int PORT = 8085;

        public static void Main(String[] args)
        {
            // Activate debug mode in console output
            Common.IO.Console.VERBOSE_LEVEL = 2;

            // Start listener for new clients
            IPAddress ipAddr = IPAddress.Parse(IP);
            Listener server = new Listener(SERVER_NAME, ipAddr, PORT);
            server.Start();

            // Start listener for user input
            UserInputListener uiListener = new UserInputListener(new IO.CommandInputHandler());
            uiListener.Start();
        }
    }
}
