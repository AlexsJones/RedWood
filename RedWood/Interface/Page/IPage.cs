using OpenQA.Selenium;
using RedWood.Interface.Debug;
using RedWood.Interface.Driver;

namespace RedWood.Interface.Page
{
    public interface IPage
    {
        IWebDriver Driver { get;  }
        ILogger Logger { get;  }
    }
}
