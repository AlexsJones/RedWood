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
        public void LogObject(ISessionDto dto)
        {
            Debug.WriteLine("Logging Session DTO [{0}]", dto);
        }
    }
}
