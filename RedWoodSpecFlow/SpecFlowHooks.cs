using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using OpenQA.Selenium;
using RedWood;
using RedWood.BootStrap;
using RedWood.Interface.Debug;
using RedWood.Interface.Driver;
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
            var container = new IoC().GetContainer();
            var logger = container.Resolve<ILogger>();

            BrowserType b = ParseEnum(ScenarioContext.Current.ScenarioInfo.Tags.First(), BrowserType.Firefox);
            var webDriver = container.ResolveKeyed<IWebDriver>(b);
                      
            ScenarioContext.Current.Set(container);
            ScenarioContext.Current.Set(logger);
            ScenarioContext.Current.Set(webDriver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            ScenarioContext.Current.Get<IWebDriver>().Close();
        }
    }
}
