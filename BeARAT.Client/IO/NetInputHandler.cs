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
        public void Handle(string input)
        {
            Common.IO.Console.Message("Received: " + input);
        }
    }
}
