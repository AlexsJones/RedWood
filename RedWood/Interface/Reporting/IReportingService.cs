using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RedWood.Interface.SessionLogger;

namespace RedWood.Interface.Reporting
{
    public interface IReportingService
    {
        void SetCurrentContext();

        void WriteLogMessage(string message);

        void WriteReport(string path);

        void WriteObject(ISessionDto dto);

        ISessionDto GetReportingSessionDto();
    }
}
