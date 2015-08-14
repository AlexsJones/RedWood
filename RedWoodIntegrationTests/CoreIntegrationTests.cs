using System;
using Autofac;
using Autofac.Core;
using NUnit.Framework;
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
using RedWood.Pages.Implementation.Page;

namespace RedWoodIntegrationTests
{
    [TestFixture]
    public class CoreIntegrationTests
    {
        [Test]
        public void BasicHeadlessDriverUsage()
        {
            var ioc = new IoC();

            ioc.RegistrationDelegate += (e) =>
            {
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

            };

            var container = new IoC().GetContainer();
            var headless = container.ResolveKeyed<IWebDriver>(BrowserType.Firefox);
            headless.Navigate().GoToUrl("http://www.google.com");
            Assert.AreEqual(headless.Title,"Google");
            headless.Quit();
        }
    }
}
