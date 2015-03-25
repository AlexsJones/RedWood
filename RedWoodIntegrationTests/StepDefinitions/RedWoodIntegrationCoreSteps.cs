using System;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using RedWood.Interface.Driver;
using TechTalk.SpecFlow;

namespace RedWoodIntegrationTests.StepDefinitions
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
           webDriver.Navigate().GoToUrl("http://www.google.com");        
        }

        [When(@"I navigate to (.*)")]
        public void WhenINavigateTo(string p0)
        {
            var webDriver = ScenarioContext.Current.Get<IWebDriver>();
            webDriver.Navigate().GoToUrl("http://www.google.com");
        }

        [Then(@"the page title should be (.*)")]
        public void ThenThePageTitleShouldBe(string p0)
        {
            var webDriver = ScenarioContext.Current.Get<IWebDriver>();
            webDriver.Navigate().GoToUrl("http://www.google.com");
            Assert.AreEqual(webDriver.Title, "Google");
            webDriver.Quit();
        }
    }
}
