using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RedWood.Implementation.Reporting
{
    [Serializable]
    public class ReportingException : Exception
    {
        public ReportingException()
        {
        }

        public ReportingException(string message)
            : base(message)
        {
        }

        public ReportingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ReportingException(string formatter, params string[] arguments)
            : base(string.Format(formatter, arguments))
        {
        }

        protected ReportingException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
