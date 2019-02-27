using BeARAT.Common.IO;
using BeARAT.Server.IO.Net;
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
            // Activate debug mode in console output
            //Common.IO.Console.VERBOSE_LEVEL = 2;

            // Start listener for new clients
            IPAddress ipAddr = IPAddress.Parse(IP);
            Listener server = new Listener(SERVER_NAME, ipAddr, PORT);
            Model.Listener = server;
            server.Start();

            // Start listener for user input
            UserInputListener uiListener = new UserInputListener(new IO.CommandInputHandler());
            uiListener.Start();
            Model.UserInputListener = uiListener;
        }

        public static void Shutdown()
        {
            Model.Listener.Stop();
            Model.UserInputListener.Stop();
        }
    }
}
