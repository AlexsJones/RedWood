using OpenQA.Selenium;
using RedWood.Pages.Interface.Page;

namespace RedWood.Pages.Implementation.Page
{
    public class Page : Expando, IPage
    {
        public delegate void TearDownDelegate(IPage page);

        private readonly KeyIdentifier[] _keyIdentifiers;
        private IWebDriver _driver;
        private string _url;
        public TearDownDelegate teardownDelegate = null;

        public Page(IWebDriver driver)
        {
            Driver = driver;
            Url = null;
        }

        public Page(IWebDriver driver, string url)
        {
            Driver = driver;
            Url = url;
        }

        public Page(IWebDriver driver, string url, KeyIdentifier[] identifiers)
        {
            Driver = driver;
            Url = url;
            _keyIdentifiers = identifiers;
        }

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
            set { _driver = value; }
        }

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
            set { _url = value; }
        }

        public KeyIdentifier[] KeyIdentifiers()
        {
            if (_keyIdentifiers == null)
            {
                throw new PageException("No key identifiers set!");
            }
            return _keyIdentifiers;
            ;
        }

        public void TearDown()
        {
            if (teardownDelegate != null)
            {
                teardownDelegate(this);
            }
        }
    }
}