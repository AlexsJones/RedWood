using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using RedWood.Pages.Implementation.Page;
using RedWood.Pages.Interface.Page;

namespace RedWoodIntegrationTests.Pages
{
    class BbcNewsPage : Page
    {
        public BbcNewsPage(IWebDriver driver) :
            base(driver,
            "news",
            new[]
            {
                new KeyIdentifier(By.LinkText("News")),
                new KeyIdentifier(By.PartialLinkText("Business")),
            })
        {
        }
    }
}
