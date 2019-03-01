using BeARAT.Common.IO.Net;
using BeARAT.Common.Concurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeARAT.Server.IO.Net
{
    public class MessageQueueHandler : Task
    {
        private const string TASK_NAME = "Message Queue Handler";
        public const int INTERVAL = 200;

        public static Queue<Message> _queue;

        private int _interval;

        public static void AddMessage(Peer peer, string data)
        {
            Message msg = new Message(peer, data);
            _queue.Enqueue(msg);
        }

        public MessageQueueHandler(int senderIntervals) : base(TASK_NAME)
        {
            _queue = new Queue<Message>();
        }

        protected override void Run()
        {
            while (this.IsRunning)
            {
                System.Threading.Thread.Sleep(_interval);
                
                if(_queue.Count > 0)
                {
                    Message msg = _queue.Dequeue();
                    msg.Peer.Send(msg.Data);
                }
            }
        }
    }
}
