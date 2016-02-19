using OpenQA.Selenium;
using RedWood.Pages.Implementation.Page;

namespace RedWoodIntegrationTests.Pages
{
    public class TestPage : Page
    {
        public TestPage(IWebDriver driver) : base(driver)
        {
        }

        public TestPage(IWebDriver driver, string url) : base(driver, url)
        {
        }
    }
}