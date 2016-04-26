using System;
using System.Reflection;
using OpenQA.Selenium;
using RedWood.Pages.Extensions.Page;
using RedWood.Pages.Implementation.Page;
using TechTalk.SpecFlow;
using FluentAssertions;
using RedWood.Pages.Extensions;

namespace RedWoodIntegrationTests.StepDefinitions.PageBaseModifiers
{
    [Binding]
    public class PageBaseModifierSteps
    {
        [Given(@"I have a base service Url (.*)")]
        public void GivenIHaveABaseServiceUrl(string p0)
        {
            ScenarioContext.Current["BASE_URL"] = p0;
        }

        [Given(@"I visit the subpage (.*)"),
        When(@"I visit the subpage (.*)"),
        Then(@"I visit the subpage (.*)"),
        Given(@"I am on relative (.*)")]
        public void GivenIVisitTheSubPage(string p0)
        {
            var webdriver = ScenarioContext.Current.Get<IWebDriver>();

            var baseUrl = (string)ScenarioContext.Current["BASE_URL"];

            baseUrl.Should().NotBeNullOrEmpty();

            var p = PageConfiguration.GetPage(Assembly.GetExecutingAssembly().GetName().Name, p0, webdriver);

            baseUrl.ParseString();
            var z = new Uri(baseUrl);

            p.Url.ParseString();
            var url = new Uri(z, p.Url);

            p.Visit(x => url.ToString());

            p.AwaitPageLoad(10);
            
            ScenarioContext.Current.Set(p);
        }

    }
}