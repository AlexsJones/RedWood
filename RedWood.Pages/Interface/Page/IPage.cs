using OpenQA.Selenium;

namespace RedWood.Pages.Interface.Page
{
    public interface IPage
    {
        IWebDriver Driver { get; set; }
        string Url { get; set; }
    }
}
