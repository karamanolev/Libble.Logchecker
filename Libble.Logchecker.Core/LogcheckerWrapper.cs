using System;
using System.Collections.Generic;
using System.Linq;
using PHP.Core;

namespace Libble.Logchecker.Core
{
    public class LogcheckerWrapper
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

        public string[] Good
        {
            get
            {
                return this.EnumerateGoodBadArray(this.logchecker.GetGood()).ToArray();
            }
        }

        public string[] Bad
        {
            get
            {
                return this.EnumerateGoodBadArray(this.logchecker.GetBad()).ToArray();
            }
        }

        public LogcheckerWrapper(string log)
        {
            this.logchecker = new LogcheckerPhp(log);
        }

        private IEnumerable<string> EnumerateGoodBadArray(object source)
        {
            foreach (KeyValuePair<IntStringKey, object> item in (PhpArray)source)
            {
                yield return (string)item.Value;
            }
        }
    }
}
