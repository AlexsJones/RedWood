using System.Reflection;
using FluentAssertions;
using OpenQA.Selenium;
using RedWood.Pages.Extensions.Page;
using RedWood.Pages.Implementation.Page;
using TechTalk.SpecFlow;

namespace RedWoodIntegrationTests.StepDefinitions.PageIdentifiers
{
    [Binding]
    public class PageIdentifiersSteps
    {
        private readonly ScenarioContext scenarioContext;

        public PageIdentifiersSteps(ScenarioContext context)
        {
            scenarioContext = context;
        }

        [Given(@"I'm on (.*)")]
        public void GivenImOn(string p0)
        {
            var webdriver = scenarioContext.Get<IWebDriver>();
            var p = PageConfiguration.GetPage(Assembly.GetExecutingAssembly().GetName().Name,
                p0, webdriver);
            p.Visit();

            scenarioContext.Set(p);
        }

        [Then(@"I should be on the correct page")]
        public void ThenIShouldBeOnTheCorrectPage()
        {
            var currentPage = scenarioContext.Get<Page>();

            currentPage.DoesPageMatchIdentifiers().Should().BeTrue();
        }
    }
}