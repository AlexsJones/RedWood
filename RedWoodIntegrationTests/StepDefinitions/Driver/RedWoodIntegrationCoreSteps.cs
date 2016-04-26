using System;
using NUnit.Framework;
using OpenQA.Selenium;
using RedWood.Pages.Extensions.Page;
using RedWoodIntegrationTests.Pages;
using TechTalk.SpecFlow;

namespace RedWoodIntegrationTests.StepDefinitions.Driver
{
    [Binding]
    public class RedWoodIntegrationCoreSteps
    {
        private readonly ScenarioContext scenarioContext;

        public RedWoodIntegrationCoreSteps(ScenarioContext context)
        {
            scenarioContext = context;
        }

        [Given(@"I have a web browser")]
        public void GivenIHaveAWebBrowser()
        {
            var testPage = new TestPage(scenarioContext.Get<IWebDriver>());
            scenarioContext.Set(testPage);
        }

        [When(@"I navigate to (.*)")]
        public void WhenINavigateTo(string p0)
        {
            var testPage = scenarioContext.Get<TestPage>();

            testPage.Visit(p0);
        }

        [Then(@"I scroll down")]
        public void ThenIScrollDown()
        {
            var testPage = scenarioContext.Get<TestPage>();
            testPage.ScrollDown();
        }

        [Then(@"I scroll up")]
        public void ThenIScrollUp()
        {
            var testPage = scenarioContext.Get<TestPage>();
            testPage.ScrollUp();
        }

        [Then(@"when I go back")]
        public void ThenWhenIGoBack()
        {
            var testPage = scenarioContext.Get<TestPage>();
            testPage.Back();
        }

        [When(@"click on (.*)")]
        public void WhenClickOn(string p0)
        {
            var testPage = scenarioContext.Get<TestPage>();
            testPage.FindElement(By.PartialLinkText(p0), TimeSpan.FromSeconds(5));
            testPage.ClickOn(By.PartialLinkText(p0));
        }

        [Then(@"the page title should be (.*)")]
        public void ThenThePageTitleShouldBe(string p0)
        {
            var testPage = scenarioContext.Get<TestPage>();

            testPage.AwaitPageLoad(5);

            var title = testPage.Title();

            Assert.True(testPage.Title().Contains(p0));
        }
    }
}