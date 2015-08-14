using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.PhantomJS;
using RedWood;
using RedWood.BootStrap;
using RedWood.Implementation.FileService;
using RedWood.Interface.Driver;
using RedWood.Interface.FileService;
using TechTalk.SpecFlow;

namespace RedWoodSpecFlow
{

    [Binding]
    public class SpecFlowHooks
    {
        public static T ParseEnum<T>(string value, T defaultValue) where T : struct
        {
            try
            {
                T enumValue;
                if (!Enum.TryParse(value, true, out enumValue))
                {
                    return defaultValue;
                }

                return enumValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        private void Load(ContainerBuilder e)
        {
            var ioc = new IoC();

            e.RegisterType<FirefoxDriver>().Keyed<IWebDriver>(BrowserType.Firefox);

            e.RegisterType<OperaDriver>().Keyed<IWebDriver>(BrowserType.Opera);

            e.RegisterType<InternetExplorerDriver>().Keyed<IWebDriver>(BrowserType.InternetExplorer).
                 WithParameters(new[]
                {
                    new ResolvedParameter((p,c) =>
                        p.Name == "internetExplorerDriverServerDirectory",
                        (p,c) => ioc.DirProject()),
                });

            e.RegisterType<ChromeDriver>().Keyed<IWebDriver>(BrowserType.Chrome).WithParameters(
                new[]
                {
                    new ResolvedParameter((p,c) =>
                        p.Name == "chromeDriverDirectory",
                        (p,c) => ioc.DirProject()),
                });

            e.RegisterType<PhantomJSDriver>().
                Keyed<IWebDriver>(BrowserType.PhantomJs).
                WithParameters(new[]
                {
                    new ResolvedParameter((p,c) =>
                        p.Name == "phantomJSDriverServerDirectory",
                        (p,c) => ioc.DirProject()),
                });
            e.RegisterType<WindowsFileService>().Keyed<IFileService>(FileServiceType.Windows);

            e.RegisterType<RemoteFileService>().Keyed<IFileService>(FileServiceType.Remote);
        }
        [BeforeScenario]
        public void BeforeScenario()
        {
            var ioc = new IoC();

            ioc.RegistrationDelegate += Load;

            var container = ioc.GetContainer();

            BrowserType b = ParseEnum(ScenarioContext.Current.ScenarioInfo.Tags.First(), BrowserType.Firefox);

            var webDriver = container.ResolveKeyed<IWebDriver>(b);

            ScenarioContext.Current.Set(container);

            ScenarioContext.Current.Set(webDriver);

        }

        [AfterScenario]
        public void AfterScenario()
        {
            ScenarioContext.Current.Get<IWebDriver>().Close();

        }
    }
}
