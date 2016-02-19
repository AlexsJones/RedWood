using OpenQA.Selenium;
using RedWood.Pages.Implementation.Page;
using RedWood.Pages.Interface.Page;

namespace RedWoodIntegrationTests.Pages
{
    internal class BbcPage : Page
    {
        public BbcPage(IWebDriver driver) :
            base(driver,
                "http://www.bbc.co.uk",
                new[]
                {
                    new KeyIdentifier(By.LinkText("News")),
                    new KeyIdentifier(By.LinkText("Sport")),
                    new KeyIdentifier(By.LinkText("Weather")),
                    new KeyIdentifier(By.LinkText("TV")),
                    new KeyIdentifier(By.PartialLinkText("More"))
                })
        {
        }
    }
}