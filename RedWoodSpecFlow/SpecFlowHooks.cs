using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using OpenQA.Selenium;
using RedWood;
using RedWood.BootStrap;
using RedWood.Implementation.SessionLogger;
using RedWood.Interface.Driver;
using RedWood.Interface.Reporting;
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

        private void LoadElasticSearch(ContainerBuilder c)
        {

        }
        [BeforeScenario]
        public void BeforeScenario()
        {
            var ioc = new IoC();

            ioc.RegistrationDelegate += LoadElasticSearch;

            var container = ioc.GetContainer();

            BrowserType b = ParseEnum(ScenarioContext.Current.ScenarioInfo.Tags.First(), BrowserType.Firefox);

            var webDriver = container.ResolveKeyed<IWebDriver>(b);


            var reportingService = container.Resolve<IReportingService>();

            reportingService.SetCurrentContext(ScenarioContext.Current.ScenarioInfo.Title);

            reportingService.WriteLogMessage(string.Format("Starting test {0}",
                ScenarioContext.Current.ScenarioInfo.Title));

            ScenarioContext.Current.Set(reportingService);

            ScenarioContext.Current.Set(container);

            ScenarioContext.Current.Set(webDriver);

        }

        [AfterScenario]
        public void AfterScenario()
        {
            ScenarioContext.Current.Get<IWebDriver>().Close();

            ScenarioContext.Current.Get<IReportingService>().WriteLogMessage("Test done");

        }
    }
}
