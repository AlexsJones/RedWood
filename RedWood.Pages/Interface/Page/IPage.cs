using OpenQA.Selenium;

namespace RedWood.Pages.Interface.Page
{
    public class KeyIdentifier
    {
        public By ByType;

        public KeyIdentifier(By type)
        {
            ByType = type;
        }
    }

    public interface IPage
    {
        IWebDriver Driver { get; set; }

        string Url { get; set; }

        KeyIdentifier[] KeyIdentifiers();

    }
}
