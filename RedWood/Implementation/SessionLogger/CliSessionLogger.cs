using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedWood.Interface.SessionLogger;

namespace RedWood.Implementation.SessionLogger
{
    class CliSessionLogger : SessionLogger, ISessionLogger
    {
        public void LogMessage(string key, string value)
        {
            Debug.WriteLine("[{0}][{1}]:{2}",
                DateTime.Now.ToUniversalTime(),key,value);
        }
    }
}
