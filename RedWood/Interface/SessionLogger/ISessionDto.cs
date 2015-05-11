using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedWood.Interface.SessionLogger
{
    public interface ISessionDto
    {
        Guid Key { get; set; }

        string Value { get; set; }

        DateTime LogSubmissionTime { get; set; }
    }
}
