using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedWood.Interface.SessionLogger;

namespace RedWood.Implementation.Reporting
{
    public class ReportingSessionDto : ISessionDto
    {
        public Guid Key { get; set; }

        public string Value { get; set; }

        public DateTime LogSubmissionTime { get; set; }
    }
}
