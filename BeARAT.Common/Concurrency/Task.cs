using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeARAT.Common.Concurrency
{
    public abstract class Task
    {
        private const string INFO_STARTED = "{0} started";
        private const string INFO_STOPPED = "{0} stopped";
        private const string INFO_ABORTED = "{0} aborted";

        protected bool IsRunning { get; set; } = true;
        protected Thread thread;
        protected int Timeout { get; set; } = 200;

        private string name;

        public Task(string name)
        {
            this.name = name;
        }

        public void Start()
        {
            thread = new Thread(new ThreadStart(Run))
            {
                Name = this.name
            };

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

        public override string ToString()
        {
            string stringRep;
            if (this.thread != null)
                stringRep = this.thread.ToString();
            else
                stringRep = this.name;
            return stringRep;
        }

        protected abstract void Run();
    }
}
