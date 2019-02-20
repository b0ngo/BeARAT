using System;
using System.IO;
using System.Net.Sockets;

namespace BeARAT.Common
{
    public class Peer
    {
        private const string FORMAT = "Peer {0} ({1}) {2}";
        private const string STATUS_CONNECTED = "connected";
        private const string STATUS_DISCONNECTED = "closed";

        public string Name { get; set; } // by default the last 32 characters of the hash
        public byte[] Hash { get; } // Sha 256 hash based on the current date time
        private int Timeout { get; set; } = 200;

        Socket socket;
        StreamReader reader;
        StreamWriter writer;

        public Peer(Socket sock) {
            socket = sock;

            Hash = Common.Hash.GetHashSha256(DateTime.Now.ToBinary());
            string hashString = Common.Hash.Hash2String(Hash);
            Name = hashString.Substring(hashString.Length - 32);

            Stream stream = new NetworkStream(socket);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        }

        public void Send(string data)
        {
            writer.WriteLine(data);
            writer.Flush();
        }

        public string Receive()
        {
            string data;
            data = reader.ReadLine();
            return data;
        }

        public bool IsAlive()
        {
            return socket.Connected;
        }

        public void Disconnect()
        {
            this.socket.Disconnect(false);
            this.socket.Close(Timeout);
        }

        public override string ToString()
        {
            String status = IsAlive() ? STATUS_CONNECTED : STATUS_DISCONNECTED;
            return string.Format(FORMAT, Name, status, socket.ToString());
        }
    }
}
