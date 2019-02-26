using System;
using System.Net;
using System.Net.Sockets;

using BeARAT.Common;

namespace BeARAT.Server
{
    class Listener : Task
    {
        private const string INFO_LISTENING = "Listening on {0}:{1}";
        private const string INFO_PEER_CONNECTED = "Peer connected: {0}";

        TcpListener listener;
        IPAddress IPAddr { get; }
        int Port { get; }

        public Listener(string name, IPAddress ipAddr, int port) : base(name)
        {
            this.IPAddr = ipAddr;
            this.Port = port;

            listener = new TcpListener(this.IPAddr, this.Port);
        }

        protected override void Run()
        {
            listener.Start();
            Common.IO.Console.Message(String.Format(INFO_LISTENING, this.IPAddr.ToString(), this.Port));

            while (IsRunning) {
                TcpClient newConnectedClient = listener.AcceptTcpClient();
                ConnectClient(newConnectedClient);
            }
        }

        private void ConnectClient(TcpClient client)
        {
            Peer p = new Peer(client);
            Server.Model.PeerMgr.AddPeer(p);
            Common.IO.Console.Message(String.Format(INFO_PEER_CONNECTED, p.ToString()));
        }
    }
}
