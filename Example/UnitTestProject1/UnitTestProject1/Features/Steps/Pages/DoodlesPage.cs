using OpenQA.Selenium;
using RedWood.Pages.Implementation.Page;
using RedWood.Pages.Interface.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Features.Steps.Pages
{
    class DoodlesPage : Page
    {
        public DoodlesPage(IWebDriver web): base(web,"doodles",
            new[] { new KeyIdentifier(By.PartialLinkText("Doodles Archive")) })
        {

        }
    }
}
