using OpenQA.Selenium;
using RedWood.Pages.Implementation.Page;
using System;
using TechTalk.SpecFlow;
using System.Dynamic;
using FluentAssertions;

namespace RedWoodIntegrationTests.StepDefinitions.DynamicPages
{
    [Binding]
    public class DynamicPagesSteps
    {
        [Given(@"I have created a dynamic page")]
        public void GivenIHaveCreatedADynamicPage()
        {

            DynamicPage dp = new DynamicPage(ScenarioContext.Current.Get<IWebDriver>());

            ScenarioContext.Current.Add("dynamicPage", dp);
        }

        [Given(@"I create a dynamic method")]
        public void GivenICreateADynamicMethod()
        {
            var dp = (dynamic)ScenarioContext.Current.Get<DynamicPage>("dynamicPage");

            dp.TestMethod  = (Func<string, string>) ((string name) => 
            {
                return "Returned Successfully: " + name;
            });
        }

        [Then(@"the expando object should work correctly when I execute it")]
        public void ThenTheExpandoObjectShouldWorkCorrectlyWhenIExecuteIt()
        {
            var dp = (dynamic)ScenarioContext.Current.Get<DynamicPage>("dynamicPage");

            var ret = dp.TestMethod("Hello");

            bool isTrue = string.Equals("Returned Successfully: " + "Hello", ret);

            isTrue.Should().BeTrue();
        }

    }
}
