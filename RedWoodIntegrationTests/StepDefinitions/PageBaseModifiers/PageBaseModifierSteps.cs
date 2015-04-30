using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using RedWood.Pages.Extensions.Page;
using RedWood.Pages.Implementation.Page;
using TechTalk.SpecFlow;

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

        [Given(@"I am on relative (.*)")]
        public void GivenIAmOnRelative(string p0)
        {
            string baseUrl = (string)ScenarioContext.Current["BASE_URL"];

            var webdriver = ScenarioContext.Current.Get<IWebDriver>();

            Page p = PageConfiguration.GetPage(Assembly.GetExecutingAssembly().GetName().Name,
                p0, webdriver);

            p.Visit(x => new Uri(new Uri(baseUrl), p.Url).ToString());

            ScenarioContext.Current.Set(p);
        }
    }
}
