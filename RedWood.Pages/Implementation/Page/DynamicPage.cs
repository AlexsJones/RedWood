using OpenQA.Selenium;
using RedWood.Pages.Interface.Page;

namespace RedWood.Pages.Implementation.Page
{
    public class DynamicPage : Page, IPage
    {
       
        public DynamicPage(IWebDriver driver): base(driver)
        {

        }
        public DynamicPage(IWebDriver driver, string url)
            : base(driver,url)
        {

        }

        public DynamicPage(IWebDriver driver, string url, KeyIdentifier[] identifiers)
            : base(driver,url,identifiers)
        {

        }

    }
}
