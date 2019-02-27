using BeARAT.Common.IO.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeARAT.Common.IO
{
    public class PeerInputPrinter : IInputHandler
    {
        private const string FORMAT = "{0}: {1}";
        private const string NULL = "<NULL>";

        private Peer peer;

        public PeerInputPrinter(Peer peer)
        {
            this.peer = peer;
        }

        public void Handle(string input)
        {
            if (input == null || input == "")
            {
                Common.IO.Console.Warning(String.Format(FORMAT, peer.ToString(), NULL));
            }
            else
            {
                Common.IO.Console.Debug(String.Format(FORMAT, peer.ToString(), input));
            }
        }
    }
}
