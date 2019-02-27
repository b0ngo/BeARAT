using BeARAT.Common.IO.Net;
using BeARAT.Common.Concurrency;

using System;
using System.IO;

namespace BeARAT.Common.IO.Net
{
    public class PeerListener : Task
    {
        private const string CONNECTION_CLOSED = "Connection {0} was closed.";
        private const string FORMAT_LISTENER = "Listener {0}";

        private Peer peer;
        private IInputHandler iHandler;

        public PeerListener(Peer peer, IInputHandler handler) : base(String.Format(FORMAT_LISTENER, peer.Name))
        {
            this.peer = peer;
            this.iHandler = handler;
        }

        protected override void Run()
        {
            while (peer.IsAlive()) {
                string receivedData = "";

                try
                {
                    receivedData += peer.Receive();
                    iHandler.Handle(receivedData);
                }
                catch (IOException)
                {
                    peer.Close();
                    Common.IO.Console.Warning(String.Format(CONNECTION_CLOSED, peer.ToString()));
                }
            }
        }
    }
}
