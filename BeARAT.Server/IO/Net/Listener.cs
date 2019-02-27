using System;
using System.Net;
using System.Net.Sockets;

using BeARAT.Common;
using BeARAT.Common.IO.Net;

namespace BeARAT.Server.IO.Net
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
                if (!listener.Pending())
                {
                    System.Threading.Thread.Sleep(500);
                    continue;
                }

                try
                {
                    TcpClient newConnectedClient = listener.AcceptTcpClient();
                    ConnectClient(newConnectedClient);
                } catch (Exception e)
                {
                    Common.IO.Console.Error(e);
                }
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
