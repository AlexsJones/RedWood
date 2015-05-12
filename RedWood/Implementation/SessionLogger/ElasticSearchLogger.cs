using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using RedWood.Interface.SessionLogger;

namespace RedWood.Implementation.SessionLogger
{
    /*
     * Redis will need to be initiated from an inherited project using RegistrationDelegate on IOC and filling out the 
     * connectionstring in the constructor 
     * The below code can be used in the delegate for userland services
     *       
     * 
     * c.RegisterType<ElasticSearchLogger>().As<ISessionLogger>().WithParameters(
                new[]
                {
                    new ResolvedParameter((a,b) => a.Name == "connectionString",
                        (a,b) => "http://192.168.22.186:5601"), 
                });
     */
    public class ElasticSearchLogger : ISessionLogger
    {
        private readonly ElasticClient _client = null;

        public ElasticSearchLogger(string connectionString)
        {
            var settings = new ConnectionSettings(new Uri(connectionString),
                defaultIndex: "logs");

            _client = new ElasticClient(settings);
              
        }

        public void LogObject(ISessionDto dto)
        {
            if (_client == null)
            {
                throw new NotImplementedException("ElasticSearch client has not implemented");
            }
            try
            {
                _client.Index(dto);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Log Message exception {0}", e.Message);
            }
        }
    }

}
