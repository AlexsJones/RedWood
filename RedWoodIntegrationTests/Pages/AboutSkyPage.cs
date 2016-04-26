using OpenQA.Selenium;
using RedWood.Pages.Implementation.Page;
using RedWood.Pages.Interface.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedWoodIntegrationTests.Pages
{
    public class AboutSkyPage : Page
    {
        public AboutSkyPage(IWebDriver driver) :
            base(driver,
                "about-sky", new[] {
                    new KeyIdentifier(By.LinkText("Our Management"))
                    })
        {
        }
    }
}
