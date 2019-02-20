using BeARAT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeARAT.Server.Communication
{
    class PeerListener : Common.Task
    {
        private const string FORMAT = "{0}: {1}";
        private const string NULL = "<NULL>";
        private Peer peer;

        public PeerListener(Peer peer) : base(peer.Name)
        {
            this.peer = peer;
        }

        protected override void Run()
        {
            while (peer.IsAlive()) {
                string receivedData = peer.Receive();

                if(receivedData == null || receivedData == "") {
                    Common.IO.Console.Warning(String.Format(FORMAT, peer.ToString(), NULL));
                }
                else {
                    Common.IO.Console.Debug(String.Format(FORMAT, peer.ToString(), receivedData));
                }
            }
        }
    }
}
