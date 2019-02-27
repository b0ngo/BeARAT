using BeARAT.Common.IO.Net;

using System;
using System.IO;

namespace BeARAT.Common.IO.Net
{
    public class PeerListener : Common.Task
    {
        private const string CONNECTION_CLOSED = "Connection {0} was closed.";

        private Peer peer;
        private IInputHandler iHandler;

        public PeerListener(Peer peer, IInputHandler handler) : base(peer.Name)
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
