using FluentAssertions;
using OpenQA.Selenium;
using RedWood.Pages.Extensions;
using RedWood.Pages.Extensions.Page;
using RedWood.Pages.Implementation.Page;
using System;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;

namespace RedWoodSpecFlow.Features.Steps
{
    [Binding]
    public class BasicSteps
    {
        private readonly ScenarioContext scenarioContext;

        public BasicSteps(ScenarioContext context)
        {
            scenarioContext = context;
        }

        [Given(@"I have a base service URL (.*)")]
        public void GivenIHaveABaseServiceURL(string path)
        {
            scenarioContext["BASE_URL"] = path;
        }
        
        [Given(@"I visit the subpage (.*)"),
        When(@"I visit the subpage (.*)"),
        Then(@"I visit the subpage (.*)")]
        public void GivenIVisitTheSubpage(string page)
        {
            var webdriver = scenarioContext.Get<IWebDriver>();

            var baseUrl = (string)scenarioContext["BASE_URL"];

            baseUrl.Should().NotBeNullOrEmpty();

            var p = PageConfiguration.GetPage(Assembly.GetExecutingAssembly().GetName().Name, page, webdriver);

            baseUrl.ParseString();
            var z = new Uri(baseUrl);

            p.Url.ParseString();
            var url = new Uri(z, p.Url);

            p.Visit(x => url.ToString());

            scenarioContext.Set(p);

        }

        [Given(@"I check the subpage and return:")]
        public void GivenICheckTheSubpageAndReturn(Table table)
        {
            foreach (var s in table.Rows)
            {
                string v = s.Values.First();

                this.GivenIVisitTheSubpage(v);

                var p = scenarioContext.Get<Page>();

                p.Driver.Navigate().Back();
            }
        }
        
        [Then(@"I am on the right page")]
        public void ThenIAmOnTheRightPage()
        {
            var p = scenarioContext.Get<Page>();

            p.DoesPageMatchIdentifiers().Should().BeTrue();

            scenarioContext.Set(p);
        }
    }
}
