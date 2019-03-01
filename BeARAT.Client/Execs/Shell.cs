using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeARAT.Common.Concurrency;

namespace BeARAT.Client.Execs
{
    abstract class Shell : Task
    {
        public Shell(string name) : base(name)
        {

        }

        public abstract void StdIn(string stdIn);
        public abstract string StdOut();
        public abstract void Execute();
        public abstract new bool IsRunning();

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected override void Run()
        {
            this.Execute();
            this.Stop();
        }
    }
}
