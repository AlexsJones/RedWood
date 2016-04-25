using Autofac;
using Autofac.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using RedWood.BootStrap;
using RedWood.Implementation.FileService;
using RedWood.Interface.Driver;
using RedWood.Interface.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace UnitTestProject1
{
    [Binding]
    public sealed class SpecflowHooks
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
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeScenario]
        public void BeforeScenario()
        {
            IoC c = new IoC();

            c.RegistrationDelegate += (cont) =>
            {
                cont.RegisterType<PhantomJSDriver>().
                Keyed<IWebDriver>(BrowserType.PhantomJs).
                 WithParameters(new[]
                     {
                    new ResolvedParameter((p, d) =>
                        p.Name == "phantomJSDriverServerDirectory",
                        (p, d) => TestContext.CurrentContext.TestDirectory)
                 });

                cont.RegisterType<WindowsFileService>().Keyed<IFileService>(FileServiceType.Windows);

                cont.RegisterType<RemoteFileService>().Keyed<IFileService>(FileServiceType.Remote);
            };

            IContainer container = c.GetContainer();

            var b = ParseEnum(ScenarioContext.Current.ScenarioInfo.Tags.First(), BrowserType.Firefox);

            var webDriver = container.ResolveKeyed<IWebDriver>(b);

            webDriver.Manage().Window.Maximize();

            ScenarioContext.Current.Set(container);

            ScenarioContext.Current.Set(webDriver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
   
            ScenarioContext.Current.Get<IWebDriver>().Quit();
        }
    }
}
