using BeARAT.Common.Concurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeARAT.Common.IO
{
    public class UserInputListener : Task
    {
        IInputHandler handler;
        bool isActive;

        public UserInputListener(IInputHandler handler) : base("User Input Handler")
        {
            this.handler = handler;
            isActive = false;
        }

        protected override void Run()
        {
            isActive = true;

            while (isActive)
            {
                string input = IO.Console.ReadLine();
                handler.Handle(input);
            }
        }
    }
}
