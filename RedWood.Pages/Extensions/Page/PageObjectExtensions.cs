using System;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RedWood.Pages.Interface.Page;

namespace RedWood.Pages.Extensions.Page
{
    public static class PageObjectExtensions
    {
        public static void Visit(this Implementation.Page.Page page)
        {
            page.Driver.Navigate().GoToUrl(page.Url);
        }

        public static void Visit(this Implementation.Page.Page page,string p0)
        {
          page.Url = p0;
          page.Driver.Navigate().GoToUrl(page.Url);
        }

        public static void Visit(this Implementation.Page.Page page, Func<string ,string> urlModifier)
        {
            page.Driver.Navigate().GoToUrl(urlModifier.Invoke(page.Url));
        }

        public static void Refresh(this Implementation.Page.Page page)
        {
            page.Driver.Navigate().Refresh();
        }

        public static void ClickOn(this Implementation.Page.Page page,By by)
        {
            page.Driver.FindElement(by).Click();
        }

        public static void Back(this Implementation.Page.Page page)
        {
            page.Driver.Navigate().Back();
        }

        public static string Title(this Implementation.Page.Page page)
        {
            return page.Driver.Title;
        }

        public static IWebElement FindElement(this Implementation.Page.Page page, By by, TimeSpan timeoutInSeconds)
        {       
            var wait = new WebDriverWait(page.Driver, timeoutInSeconds);
            return wait.Until(driver => driver.FindElement(by));
        }

        public static bool DoesPageMatchIdentifiers(this Implementation.Page.Page page)
        {
            if (page.KeyIdentifiers().Length == 0) return false;
            foreach (var id in page.KeyIdentifiers())
            {
                try
                {
                    FindElement(page, id.ByType, TimeSpan.FromSeconds(10));
                }
                catch
                {
                    Debug.WriteLine("DoesPageMatchIdentifiers not found => " + id.ByType);
                    return false;
                }
            }
            return true;
        }

        public static string ResolveFullUrl(this Implementation.Page.Page page, string baseUrl)
        {
            return Path.Combine(baseUrl, page.Url);
        }
    }
}
