using System;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Extras.NLog;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.PhantomJS;
using RedWood.Implementation.FileService;
using RedWood.Interface.Driver;
using RedWood.Interface.FileService;

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
            var dirDebug = Directory.GetCurrentDirectory();

            var dirProject = dirDebug;

            for (var counterSlash = 0; counterSlash < 2; counterSlash++)
            {
                dirProject = dirProject.Substring(0, dirProject.LastIndexOf(@"\", StringComparison.Ordinal));
            }

            return dirProject;
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