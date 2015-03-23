using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.NLog;
using RedWood.Implementation.Debug;
using ILogger = RedWood.Interface.Debug.ILogger;

namespace RedWood
{
    public class IoC
    {
        ContainerBuilder GetContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<NLogModule>();
            containerBuilder.RegisterType<Logger>().As<ILogger>();
            return containerBuilder;
        }
        public IContainer GetContainer()
        {
            var container = GetContainerBuilder().Build();

            return container;
        }
    }
}
