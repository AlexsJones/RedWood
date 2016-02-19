using System.IO;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Extras.NLog;
using log4net;

namespace RedWood.BootStrap
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(LogManager.GetLogger("RedWoodFileLogger"));
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry,
            IComponentRegistration registration)
        {
            registration.Preparing += registration_Preparing;
        }

        private static void registration_Preparing(object sender, PreparingEventArgs e)
        {
            var t = e.Component.Activator.LimitType;
            e.Parameters = e.Parameters.Union(
                new[]
                {
                    new ResolvedParameter((p, i) => p.ParameterType == typeof (ILog), (p, i) => LogManager.GetLogger(t))
                });
        }
    }

    public class IoC
    {
        public delegate void RegisterThirdPartyDependencies(ContainerBuilder c);

        public RegisterThirdPartyDependencies RegistrationDelegate = null;

        public string DirProject()
        {
            return Directory.GetCurrentDirectory();
        }

        private ContainerBuilder GetContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();

            /* NLog module */
            containerBuilder.RegisterModule<NLogModule>();

            return containerBuilder;
        }

        public IContainer GetContainer()
        {
            var container = GetContainerBuilder();

            if (RegistrationDelegate != null)
                RegistrationDelegate(container);

            container.RegisterModule(new ApplicationModule());

            return container.Build();
        }
    }
}