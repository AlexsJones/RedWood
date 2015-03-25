using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using RedWood;
using RedWood.BootStrap;
using RedWood.Interface.Debug;
using TechTalk.SpecFlow;

namespace RedWoodSpecFlow
{
    [Binding]
    public class SpecFlowHooks
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            var container = new IoC().GetContainer();
            var logger = container.Resolve<ILogger>();

            ScenarioContext.Current.Set(container);
            ScenarioContext.Current.Set(logger);
        }

        [AfterScenario]
        public void AfterScenario()
        {
        }
    }
}
