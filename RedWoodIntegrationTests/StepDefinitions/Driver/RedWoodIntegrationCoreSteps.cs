using System;
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
           var testPage = new TestPage(ScenarioContext.Current.Get<IWebDriver>());
           ScenarioContext.Current.Set(testPage);
        }
        [When(@"I navigate to (.*)")]
        public void WhenINavigateTo(string p0)
        {
            var testPage = ScenarioContext.Current.Get<TestPage>();
            testPage.Visit(p0);
        }
        [Then(@"when I go back")]
        public void ThenWhenIGoBack()
        {
            var testPage = ScenarioContext.Current.Get<TestPage>();
            testPage.Back();
        }
        [When(@"click on (.*)")]
        public void WhenClickOn(string p0)
        {
            var testPage = ScenarioContext.Current.Get<TestPage>();
            testPage.FindElement(By.PartialLinkText(p0), TimeSpan.FromSeconds(3));
            testPage.ClickOn(By.PartialLinkText(p0));
        }
        [Then(@"the page title should be (.*)")]
        public void ThenThePageTitleShouldBe(string p0)
        {
            var testPage = ScenarioContext.Current.Get<TestPage>();
            Assert.True(testPage.Title().Contains(p0));        
        }
    }
}
