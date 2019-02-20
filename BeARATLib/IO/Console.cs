using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeARAT.Common.IO
{
    public static class Console
    {
        public enum Flag { MESSAGE, WARNING, ERROR, DEBUG };
        /*
         * Level 0 : no system output
         * Level 1 : general output (+ messages, warninngs, exception)
         * Level 2 : debug mode ( + exception stacktrace )
         */
        public static int VERBOSE_LEVEL { get; set; } = 1;

        private static string LF = Environment.NewLine;
        private static string FLAG_MESSAGE = "[   ]";
        private static string FLAG_WARNING = "[ ! ]";
        private static string FLAG_ERROR = "[ERR]";
        private static string FLAG_DEBUG = "[ > ]";

        public static void Print(Flag f, string msg) {
            StringBuilder strBuilder = new StringBuilder();

            if(VERBOSE_LEVEL == 0) {
                return;
            }

            if(VERBOSE_LEVEL == 1 && f != Flag.DEBUG) {
                strBuilder.Append(FlagToString(f));
                strBuilder.Append(' ');
                strBuilder.Append(msg);
            }

            if(VERBOSE_LEVEL == 2 && f == Flag.DEBUG) {
                strBuilder.Append(FlagToString(f));
                strBuilder.Append(' ');
                strBuilder.Append(msg);
            }

            Print(strBuilder.ToString());
        }

        public static void Message(string msg)
        {
            Print(Flag.MESSAGE, msg);
        }

        public static void Warning(string msg)
        {
            Print(Flag.WARNING, msg);
        }

        public static void Error(Exception e)
        {
            Print(Flag.ERROR, e.Message);
            Debug(e.Source);

            string[] stacktrace = e.StackTrace.Split(LF.ToCharArray());

            for(int i = 0; i < stacktrace.Length; i++) {
                Debug(stacktrace[i]);
            }
        }

        public static void Debug(string msg)
        {
            Print(Flag.DEBUG, msg);
        }

        private static void Print(string line)
        {
            System.Console.WriteLine(line);
        }

        private static string FlagToString(Flag flag)
        {
            switch (flag) {
                case Console.Flag.WARNING:
                    return Console.FLAG_WARNING;
                case Console.Flag.ERROR:
                    return Console.FLAG_ERROR;
                case Console.Flag.DEBUG:
                    return Console.FLAG_DEBUG;
                default:
                    return Console.FLAG_MESSAGE;
            }
        }
    }
}
