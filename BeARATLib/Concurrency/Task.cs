using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeARAT.Common
{
    public abstract class Task
    {
        private const string INFO_STARTED = "{0} started";
        private const string INFO_STOPPED = "{0} stopped";
        private const string INFO_ABORTED = "{0} aborted";

        protected bool IsRunning { get; set; } = true;
        protected Thread thread;
        protected int Timeout { get; set; } = 200;

        public Task(string name)
        {
            thread = new Thread(new ThreadStart(Run)) {
                Name = name
            };
        }

        public void Start()
        {
            thread.Start();
            Common.IO.Console.Message(String.Format(INFO_STARTED, thread.Name));
        }

        public void Stop()
        {
            IsRunning = false;

            if (thread.Join(Timeout))
                Common.IO.Console.Message(String.Format(INFO_STOPPED, thread.Name));
            else {
                thread.Abort();
                Common.IO.Console.Warning(String.Format(INFO_ABORTED, thread.Name));
            }
        }

        protected abstract void Run();
    }
}
