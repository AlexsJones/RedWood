using OpenQA.Selenium;
using RedWood.Pages.Interface.Page;

namespace RedWood.Pages.Implementation.Page
{
    public class Page : Expando, IPage
    {
        public delegate void TearDownDelegate(IPage page);

        public TearDownDelegate teardownDelegate = null;

        private IWebDriver _driver = null;

        private readonly KeyIdentifier[] _keyIdentifiers = null;

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

        public KeyIdentifier[] KeyIdentifiers()
        {
            if (_keyIdentifiers == null)
            {
                throw new PageException("No key identifiers set!");
            }
            return _keyIdentifiers;
            ;
        }

        public Page(IWebDriver driver): base()
        {
            Driver = driver;
            Url = null;
        }
        public Page(IWebDriver driver,string url): base()
        {
            Driver = driver;
            Url = url;
        }

        public Page(IWebDriver driver, string url, KeyIdentifier[] identifiers): base()
        {
            Driver = driver;
            Url = url;
            _keyIdentifiers = identifiers;
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
