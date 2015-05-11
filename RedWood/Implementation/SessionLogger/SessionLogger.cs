using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedWood.Implementation.SessionLogger
{
    public class SessionLogger
    {
        public string GenerateGuidDateStampKeyString()
        {
            return string.Format("{0}:{1}", Guid.NewGuid()
                 , (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }
    }
}
