using System;
using FluentAssertions;
using OpenQA.Selenium;
using RedWood.Pages.Implementation.Page;
using TechTalk.SpecFlow;

namespace RedWoodIntegrationTests.StepDefinitions.DynamicPages
{
    [Binding]
    public class DynamicPagesSteps
    {

        private readonly ScenarioContext scenarioContext;

        public DynamicPagesSteps(ScenarioContext context)
        {
            scenarioContext = context;
        }

        [Given(@"I have created a dynamic page")]
        public void GivenIHaveCreatedADynamicPage()
        {
            var dp = new DynamicPage(scenarioContext.Get<IWebDriver>());

            scenarioContext.Add("dynamicPage", dp);
        }

        [Given(@"I create a dynamic method")]
        public void GivenICreateADynamicMethod()
        {
            var dp = (dynamic)scenarioContext.Get<DynamicPage>("dynamicPage");

            dp.TestMethod = (Func<string, string>) ((string name) => { return "Returned Successfully: " + name; });
        }

        [Then(@"the expando object should work correctly when I execute it")]
        public void ThenTheExpandoObjectShouldWorkCorrectlyWhenIExecuteIt()
        {
            var dp = (dynamic)scenarioContext.Get<DynamicPage>("dynamicPage");

            var ret = dp.TestMethod("Hello");

            bool isTrue = string.Equals("Returned Successfully: " + "Hello", ret);

            isTrue.Should().BeTrue();
        }
    }
}