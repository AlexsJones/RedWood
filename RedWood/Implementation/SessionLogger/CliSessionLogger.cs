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
                GenerateGuidDateStampKeyString(),
                message);
        }

        public string GenerateGuidDateStampKeyString()
        {
            return string.Format("{0}:{1}", Guid.NewGuid()
                 , (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }
    }
}
