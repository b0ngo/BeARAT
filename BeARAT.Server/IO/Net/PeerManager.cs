using BeARAT.Common;
using System.Collections.Generic;
using BeARAT.Common.IO.Net;
using BeARAT.Common.IO;

namespace BeARAT.Server.IO.Net
{
    class PeerManager
    {
        private int SessionIndex { get; set; } = -1;
        private List<Peer> Peers { get; set; }

        public PeerManager()
        {
            Peers = new List<Peer>();
        }

        public void AddPeer(Peer peer)
        {
            Peers.Add(peer);
            PeerInputPrinter printer = new PeerInputPrinter(peer);
            PeerListener listener = new PeerListener(peer, printer);
            listener.Start();

            if(Peers.Count == 1)
                SessionIndex = 0;
        }

        public Peer GetPeer()
        {
            return GetPeer(SessionIndex);
        }

        public Peer GetPeer(int i)
        {
            return Peers[i];
        }

        public int GetPeerSize()
        {
            return Peers.Count;
        }

        public void SetPeerSession(int sessionIndex)
        {
            this.SessionIndex = sessionIndex;
        }
    }
}
