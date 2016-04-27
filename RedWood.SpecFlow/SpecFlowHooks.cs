using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using RedWood.BootStrap;
using RedWood.Implementation.FileService;
using RedWood.Interface.Driver;
using RedWood.Interface.FileService;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace RedWoodSpecFlow
{
    [Binding]
    public class SpecFlowHooks
    {
        private readonly ScenarioContext specflowContext;

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


        public SpecFlowHooks(ScenarioContext context)
        {
            specflowContext = context;
        }

        private void Load(ContainerBuilder e)
        {
            var ioc = new IoC();

            e.RegisterType<FirefoxDriver>().Keyed<IWebDriver>(BrowserType.Firefox).WithParameters(
                new[]
                {
                    new ResolvedParameter((p, c) =>
                        p.Name == "binary",
                        (p, c) => new FirefoxBinary("C:\\Program Files (x86)\\Mozilla Firefox\\firefox.exe")),
                    new ResolvedParameter((p, c) =>
                        p.Name == "profile",
                        (p, c) => new FirefoxProfile())
                });

            e.RegisterType<InternetExplorerDriver>().Keyed<IWebDriver>(BrowserType.InternetExplorer).
                WithParameters(new[]
                {
                    new ResolvedParameter((p, c) =>
                        p.Name == "internetExplorerDriverServerDirectory",
                        (p, c) => TestContext.CurrentContext.TestDirectory)
                });

            e.RegisterType<ChromeDriver>().Keyed<IWebDriver>(BrowserType.Chrome).WithParameters(
                new[]
                {
                    new ResolvedParameter((p, c) =>
                        p.Name == "chromeDriverDirectory",
                        (p, c) => TestContext.CurrentContext.TestDirectory)
                });

            e.RegisterType<PhantomJSDriver>().
                Keyed<IWebDriver>(BrowserType.PhantomJs).
                WithParameters(new[]
                {
                    new ResolvedParameter((p, c) =>
                        p.Name == "phantomJSDriverServerDirectory",
                        (p, c) => TestContext.CurrentContext.TestDirectory)
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

            var b = ParseEnum(specflowContext.ScenarioInfo.Tags.First(), BrowserType.Firefox);

            var webDriver = container.ResolveKeyed<IWebDriver>(b);

            webDriver.Manage().Window.Maximize();

            specflowContext.Set(container);

            specflowContext.Set(webDriver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            specflowContext.Get<IWebDriver>().Quit();

        }
    }
}