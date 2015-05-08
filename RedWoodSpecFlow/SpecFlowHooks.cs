using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using OpenQA.Selenium;
using RedWood;
using RedWood.BootStrap;
using RedWood.Interface.Driver;
using RedWood.Interface.SessionLogger;
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

        [BeforeScenario]
        public void BeforeScenario()
        {
            var ioc = new IoC();

            var container = ioc.GetContainer();

            BrowserType b = ParseEnum(ScenarioContext.Current.ScenarioInfo.Tags.First(), BrowserType.Firefox);

            var webDriver = container.ResolveKeyed<IWebDriver>(b);

            var sessionLogger = container.Resolve<ISessionLogger>();

            ScenarioContext.Current.Set(container);

            ScenarioContext.Current.Set(sessionLogger);

            ScenarioContext.Current.Set(webDriver);

            sessionLogger.LogMessage(string.Format("Starting scenario => {0}",
                ScenarioContext.Current.ScenarioInfo.Title));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            ScenarioContext.Current.Get<IWebDriver>().Close();

            ScenarioContext.Current.Get<ISessionLogger>().LogMessage(
                string.Format("Starting scenario => {0}",
    ScenarioContext.Current.ScenarioInfo.Title));
        }
    }
}
