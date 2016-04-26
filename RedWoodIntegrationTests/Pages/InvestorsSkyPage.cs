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
    public class InvestorsSkyPage : Page
    {
        public InvestorsSkyPage(IWebDriver driver) :
            base(driver,
                "investors", new[] {
                    new KeyIdentifier(By.LinkText("Latest results"))
                    })
        {
        }
    }
}
