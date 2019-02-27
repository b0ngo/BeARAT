using System;
using System.IO;
using System.Net.Sockets;
using BeARAT.Client.IO;
using BeARAT.Common.IO.Net;

namespace BeARAT.Client
{
    class Program
    {
        private const string INVALID_SETUP = "Invalid usage of the client";

        private static PeerListener listener;

        public static void Main(string[] args)
        {
            string host;
            int port;

            // activate debug mode
            Common.IO.Console.VERBOSE_LEVEL = 2;

            try
            {
                host = args[0];
                port = int.Parse(args[1]);
            } catch(Exception)
            {
                Common.IO.Console.Warning(INVALID_SETUP);
                return;
            }

            try {
                setup(host, port);

            } catch (Exception e) {
                Common.IO.Console.Error(e);
            }
        }

        private static void setup(string host, int port)
        {
            Common.IO.Console.Message("Connecting...");
            TcpClient client = new TcpClient(host, port);
            Common.IO.Console.Message("Connected");

            Peer peer = new Peer(client);
            NetInputHandler iHandler = new NetInputHandler();
            listener = new PeerListener(peer, iHandler);
            listener.Start();

            Model.Peer = peer;
            Model.Listener = listener;
        }
    }
}
