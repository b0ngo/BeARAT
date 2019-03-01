using BeARAT.Common.IO;
using BeARAT.Server.IO.Net;

namespace BeARAT.Server
{
    static class Model
    {
        public static PeerManager PeerMgr { get; } = new PeerManager();
        public static Listener Listener { get; set; }
        public static MessageQueueHandler MessageQueueHandler { get; set; }
        public static UserInputListener UserInputListener { get; set; }
    }
}
