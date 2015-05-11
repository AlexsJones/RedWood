using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedWood.Interface.SessionLogger;

namespace RedWood.Implementation.SessionLogger
{
    public class SessionInfo : ISessionInfo
    {
        private readonly Guid _guid;
        private readonly Stopwatch _creationTime = null;
        public SessionInfo()
        {
            _guid = Guid.NewGuid();

            _creationTime = new Stopwatch();

            _creationTime.Start();
        }

        public string GetCurrentGuidTimeString()
        {
            return string.Format("{0}:{1}", 
                _guid, _creationTime.ElapsedMilliseconds);
        }
    }

    public class SessionLogger
    {
        public ISessionInfo GenerateSessionInfo()
        {
            return new SessionInfo();
        }
    }
}
