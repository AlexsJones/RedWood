using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedWood.Interface.SessionLogger;

namespace RedWood.Implementation.SessionLogger
{
    class CliSessionLogger : ISessionLogger
    {
        public void LogMessage(string message)
        {
            Debug.WriteLine("[{0}][{1}]:{2}",
                DateTime.Now.ToUniversalTime(),
                Guid.NewGuid().ToString(),
                message);
        }
    }
}
