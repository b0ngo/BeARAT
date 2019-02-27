using BeARAT.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeARAT.Server
{
    static class Model
    {
        public static PeerManager PeerMgr { get; } = new PeerManager();
        public static Listener Listener { get; set; }
        public static UserInputListener UserInputListener { get; set; }
    }
}
