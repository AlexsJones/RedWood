using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Extras.NLog;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using RedWood.Implementation.Debug;
using RedWood.Interface.Driver;
using ILogger = RedWood.Interface.Debug.ILogger;
using IWebDriver = OpenQA.Selenium.IWebDriver;

namespace RedWood
{
    public class IoC
    {
        ContainerBuilder GetContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            
            containerBuilder.RegisterModule<NLogModule>();
           
            containerBuilder.RegisterType<Logger>().As<ILogger>();

            containerBuilder.RegisterType<FirefoxDriver>().Keyed<IWebDriver>(BrowserType.Firefox);

            containerBuilder.RegisterType<ChromeDriver>().Keyed<IWebDriver>(BrowserType.Chrome);

            containerBuilder.RegisterType<PhantomJSDriver>().
                Keyed<IWebDriver>(BrowserType.PhantomJS).
                WithParameters(new []
                {
                    new ResolvedParameter((p,c) => 
                        p.Name == "phantomJSDriverServerDirectory",
                        (p,c) => @"..\..\..\ThirdParty"),
                });          

            return containerBuilder;
        }
     
        public IContainer GetContainer()
        {
            var container = GetContainerBuilder().Build();
            return container;
        }
    }
}
