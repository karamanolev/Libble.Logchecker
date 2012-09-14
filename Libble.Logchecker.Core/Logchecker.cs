using System;
using System.Linq;

namespace Libble.Logchecker.Core
{
    public class Logchecker
    {
        private LogcheckerPhp logchecker;

        public int Score
        {
            get { return (int)logchecker.GetScore(); }
        }

        public string Log
        {
            get { return (string)logchecker.GetLog(); }
        }

        public Logchecker(string log)
        {
            this.logchecker = new LogcheckerPhp(log);
        }
    }
}
