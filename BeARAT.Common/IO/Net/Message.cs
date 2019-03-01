using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeARAT.Common.IO.Net
{
    public class Message
    {
        private string _data;
        private Peer _peer;

        public Message(Peer peer, string data)
        {
            _data = data;
            _peer = peer;
        }

        public string Data { get => _data; }
        public Peer Peer { get => _peer; }
    }
}
