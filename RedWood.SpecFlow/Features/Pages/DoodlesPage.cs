using OpenQA.Selenium;
using RedWood.Pages.Implementation.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedWoodSpecFlow.Features.Pages
{
    public class DoodlesPage : Page
    {
        public DoodlesPage(IWebDriver driver) : base(driver, "doodles", 
            new[] {
                new RedWood.Pages.Interface.Page.KeyIdentifier(By.LinkText("Doodles Archive"))
            })
        {

        }
    }
}
