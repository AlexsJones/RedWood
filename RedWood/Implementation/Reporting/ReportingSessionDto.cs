using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using RedWood.Interface.SessionLogger;

namespace RedWood.Implementation.Reporting
{
    public class ReportingSessionDto : ISessionDto
    {
        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed, Store = true)]
        public Guid Key { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed, Store = true)]
        public string TestName { get; set; }

        [ElasticProperty(Index = FieldIndexOption.NotAnalyzed, Store = true)]
        public string Value { get; set; }

        public DateTime LogSubmissionTime { get; set; }
    }
}
