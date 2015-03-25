using Autofac;
using NUnit.Framework;
using OpenQA.Selenium;
using RedWood.Interface.Driver;
using RedWood.Pages.Extensions.Page;
using RedWoodIntegrationTests.Pages;
using TechTalk.SpecFlow;

namespace RedWoodIntegrationTests.StepDefinitions.Driver
{
    [Binding]
    public class RedWoodIntegrationCoreSteps
    {
        [Given(@"I have a web browser")]
        public void GivenIHaveAWebBrowser()
        {
           var container =  ScenarioContext.Current.Get<IContainer>();
           var webDriver = container.ResolveKeyed<IWebDriver>(BrowserType.PhantomJs);
           ScenarioContext.Current.Set(webDriver);

           var testPage = new TestPage(webDriver,"http://www.google.com");
           ScenarioContext.Current.Set(testPage);
        }

        [When(@"I navigate to (.*)")]
        public void WhenINavigateTo(string p0)
        {
            var testPage = ScenarioContext.Current.Get<TestPage>();
            testPage.Visit();
        }

        [Then(@"the page title should be (.*)")]
        public void ThenThePageTitleShouldBe(string p0)
        {
            var testPage = ScenarioContext.Current.Get<TestPage>();
            Assert.AreEqual(testPage.Title(), "Google");
            testPage.Driver.Quit();
        }
    }
}
