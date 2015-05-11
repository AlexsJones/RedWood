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
     * The below code can be used in the delegate for userland services
     *       
     * 
     * c.RegisterType<RedisSessionLogger>().As<ISessionLogger>().WithParameters(
                new[]
                {
                    new ResolvedParameter((a,b) => a.Name == "connectionString",
                        (a,b) => "192.168.22.186:6379"), 
                });
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
            _redis.GetDatabase().StringSetAsync(GenerateGuidDateStampKeyString(),
                message);
        }

        public string GenerateGuidDateStampKeyString()
        {
            return string.Format("{0}:{1}", Guid.NewGuid()
                 , (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }
    }
}
