using System;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;

namespace RedWood.Pages.Implementation.Page
{
    public class PageConfiguration
    {
        public static Page GetPage(string assemblyname, string name, IWebDriver driver)
        {
            var type = Assembly.Load(assemblyname).GetTypes().First(t => t.Name == name);
            if (type == null)
            {
                throw new PageException("Page Not found!");
            }
            return (Page) Activator.CreateInstance(type, driver);
        }
    }
}