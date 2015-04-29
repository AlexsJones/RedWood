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
    class BbcPage : Page
    {      
        public BbcPage(IWebDriver driver) :
            base(driver, 
            "http://www.bbc.co.uk",
            new []
            {
                new KeyIdentifier(By.LinkText("News")),
                new KeyIdentifier(By.LinkText("Sport")),
                new KeyIdentifier(By.LinkText("Weather")),
                new KeyIdentifier(By.LinkText("TV")),
                new KeyIdentifier(By.PartialLinkText("More")),
            })
        {
        }
    }
}
