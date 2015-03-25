using OpenQA.Selenium;
using RedWood.Pages.Interface.Page;

namespace RedWood.Pages.Implementation.Page
{
    public class Page : IPage
    {
        private IWebDriver _driver = null;
        public IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    throw new PageException("Driver is null");
                }
                return _driver;
            }
            set
            {
                _driver = value;
            }           
        }
        private string _url = null;
        public string Url
        {
            get
            {
                if (_url == null)
                {
                    throw new PageException("Driver is null");
                }
                return _url;
            }
            set
            {
                _url = value;
            }
        }

        public Page(IWebDriver driver)
        {
            Driver = driver;
            Url = null;
        }
        public Page(IWebDriver driver,string url)
        {
            Driver = driver;
            Url = url;
        }
    }
}
