using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedWood.Interface.SessionLogger
{
    public interface ISessionInfo
    {
         string GetCurrentGuidTimeString();
    }
    public interface ISessionLogger
    {
        void LogMessage(string key, string value);

        ISessionInfo GenerateSessionInfo();
    }
}
