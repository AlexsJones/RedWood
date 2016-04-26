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
    public class CorporateSkyPage : Page
    {
        public CorporateSkyPage(IWebDriver driver) :
            base(driver,
                ".",new[] {
                    new KeyIdentifier(By.LinkText("About Sky"))
                    })
        {
        }
    }
}
