using Autofac;
using Autofac.Core;
using Autofac.Extras.NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.PhantomJS;
using RedWood.Interface.Driver;


namespace RedWood.BootStrap
{
    public class IoC
    {
        ContainerBuilder GetContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            
            containerBuilder.RegisterModule<NLogModule>();

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
