using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RedWood.Interface.Reporting
{
    public interface IReportingService
    {
        void SetCurrentContext();

        void WriteLogMessage(string message);

        void WriteReport(string path);
    }
}
