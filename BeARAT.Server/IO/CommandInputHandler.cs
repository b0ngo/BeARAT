using BeARAT.Common.IO;
using BeARAT.Common.IO.Net;
using System;
using System.IO;
using System.Text;

namespace BeARAT.Server.IO
{
    class CommandInputHandler : IInputHandler
    {
        private const char CMD_SEPARATOR = ' ';

        private const string CMD_SET_SESSION = "session";
        private const string CMD_SHOW_SESSION = "sessions";
        private const string CMD_EXIT = "exit";

        private string[] cmd;

        public CommandInputHandler()
        {

        }

        public void Handle(string input)
        {
            cmd = input.Split(CMD_SEPARATOR);

            try
            {
                switch (cmd[0])
                {
                    case CMD_EXIT:
                        Program.Shutdown();
                        break;
                    case CMD_SET_SESSION:
                        SetSession();
                        break;
                    case CMD_SHOW_SESSION:
                        ShowSessions();
                        break;
                    default:
                        Send();
                        break;
                }
            } catch(Exception e)
            {
                Common.IO.Console.Error(e);
            }
            
        }

        private void SetSession()
        {
            if (cmd.Length != 2)
                throw new ArgumentException();

            int sessionIndex;
            if (!int.TryParse(cmd[1], out sessionIndex))
                throw new ArgumentException();

            if (sessionIndex >= Model.PeerMgr.GetPeerSize())
                throw new ArgumentException();

            Model.PeerMgr.SetPeerSession(sessionIndex);
        }

        private void ShowSessions()
        {
            if (cmd.Length != 1)
                throw new ArgumentException();

            const string format = "\t{0}\t{1}\t{2}\t{3}\t{4}";

            Common.IO.Console.Message(string.Format(format, "No", "Active", "Connected", "ID", "Name"));

            for (int i = 0; i < Model.PeerMgr.GetPeerSize(); i++)
            {
                Peer p = Model.PeerMgr.GetPeer(i);
                string active = p.Equals(Model.PeerMgr.GetPeer(i)) ? "(*)" : "   ";
                string connected = p.IsAlive() ? "(*)" : "   ";
                string line = string.Format(format, i, active, connected, p.Hash, p.Name);
                Common.IO.Console.Message(line);
            }
        }

        private void Send()
        {
            StringBuilder bld = new StringBuilder();
            for(int i = 0; i < cmd.Length; i++)
            {
                bld.Append(cmd[i]);
                bld.Append(" ");
            }

            Model.PeerMgr.GetPeer().Send(bld.ToString());
        }
    }
}
