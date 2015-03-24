using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace RedWood.Interface.Driver
{
    public enum BrowserType
    {
        Chrome,
        Firefox
    };
    public interface IDriver : IWebDriver
    {
        
    }
}
