﻿using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Extras.NLog;
using NLog.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.PhantomJS;
using RedWood.Interface.Driver;
using System.Configuration;
using RedWood.Implementation.FileService;
using RedWood.Interface.FileService;

namespace RedWood.BootStrap
{
    public class IoC
    {

        public delegate void RegisterThirdPartyDependencies(ContainerBuilder c);

        public RegisterThirdPartyDependencies RegistrationDelegate = null;

        public string DirProject()
        {
            string dirDebug = System.IO.Directory.GetCurrentDirectory();
            var dirProject = dirDebug;

            for (int counter_slash = 0; counter_slash < 2; counter_slash++)
            {
                dirProject = dirProject.Substring(0, dirProject.LastIndexOf(@"\"));
            }

            return dirProject;
        }

        ContainerBuilder GetContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            
            /* NLog module */
            containerBuilder.RegisterModule<NLogModule>();

            /* Web driver */
            containerBuilder.RegisterType<FirefoxDriver>().Keyed<IWebDriver>(BrowserType.Firefox);

            containerBuilder.RegisterType<OperaDriver>().Keyed<IWebDriver>(BrowserType.Opera);

            containerBuilder.RegisterType<InternetExplorerDriver>().Keyed<IWebDriver>(BrowserType.InternetExplorer);

            containerBuilder.RegisterType<ChromeDriver>().Keyed<IWebDriver>(BrowserType.Chrome);

            containerBuilder.RegisterType<PhantomJSDriver>().
                Keyed<IWebDriver>(BrowserType.PhantomJs).
                WithParameters(new []
                {
                    new ResolvedParameter((p,c) => 
                        p.Name == "phantomJSDriverServerDirectory",
                        (p,c) => DirProject()),
                });
            
            /* File services */
            containerBuilder.RegisterType<LocalFileService>().Keyed<IFileService>(FileServiceType.Local);

            containerBuilder.RegisterType<RemoteFileService>().Keyed<IFileService>(FileServiceType.Remote);

            return containerBuilder;
        }



        public IContainer GetContainer()
        {
            var container = GetContainerBuilder();
          
            if(RegistrationDelegate != null)
                RegistrationDelegate(container);

            return container.Build();
        }
    }
}
