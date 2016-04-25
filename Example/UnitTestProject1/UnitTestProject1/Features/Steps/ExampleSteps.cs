using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using RedWood.Pages;
using FluentAssertions;
using RedWood.Pages.Implementation.Page;
using System.Reflection;
using RedWood.Pages.Extensions.Page;
using UnitTestProject1.Extensions;

namespace UnitTestProject1.Features.Steps
{
    [Binding]
    public class ExampleSteps
    {
        [Given(@"I have a base service URL (.*)")]
        public void GivenIHaveABaseServiceURL(string path)
        {
            ScenarioContext.Current["BASE_URL"] = path;
        }

        [Given(@"I visit the subpage (.*)"),
        When(@"I visit the subpage (.*)"),
        Then(@"I visit the subpage (.*)")]
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

            ScenarioContext.Current.Set(p);
        }

        [Given(@"I am on the right page"),
        When(@"I am on the right page"),
        Then(@"I am on the right page")]
        public void ThenIAmOn()
        {
            var p = ScenarioContext.Current.Get<Page>();

            p.DoesPageMatchIdentifiers().Should().BeTrue();

            ScenarioContext.Current.Set(p);
        }
    }
}
