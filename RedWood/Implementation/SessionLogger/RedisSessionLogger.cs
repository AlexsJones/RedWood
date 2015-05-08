using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedWood.Interface.SessionLogger;
using StackExchange.Redis;

namespace RedWood.Implementation.SessionLogger
{
    /*
     * Redis will need to be initiated from an inherited project using RegistrationDelegate on IOC and filling out the 
     * connectionstring in the constructor 
     */
    public class RedisSessionLogger : ISessionLogger
    {
        private readonly ConnectionMultiplexer _redis = null;

        public RedisSessionLogger(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
        }
        public void LogMessage(string message)
        {
            if (_redis == null)
            {
                throw new NotImplementedException("Redis session logger has not implemented connection string in constructor");
            }
            _redis.GetDatabase().StringSetAsync(Guid.NewGuid().ToString(),
                message);
        }
    }
}
