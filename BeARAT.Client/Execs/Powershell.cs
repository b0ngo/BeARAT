using System;
using System.Diagnostics;

namespace BeARAT.Client.Execs
{
    /**
     * Cancelled the implementation of Powershell for now.
     * Generally, it can be realized by using System.Management.Automation.Powershell
     */
    class Powershell : Shell
    {
        private const string FILENAME = "powershell.exe";

        Process ps;

        string input;
        string output;
        bool isRunning;

        public Powershell() : base(FILENAME)
        {
            ps = new Process();
            ps.StartInfo.FileName = FILENAME;

            input = "";
            output = "";
            isRunning = false;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override string StdOut()
        {
            return this.output;
        }

        public override bool IsRunning()
        {
            return this.isRunning;
        }

        public override void StdIn(string input)
        {
            this.input = input;
        }
    }
}
