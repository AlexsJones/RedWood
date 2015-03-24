using System;
using System.Drawing.Printing;
using Autofac;
using NUnit.Framework;
using OpenQA.Selenium;
using RedWood;
using RedWood.Interface.Driver;
using Selenium_SpecFlow.Support;


namespace RedWoodIntegrationTests
{
    [TestFixture]
    public class CoreIntegrationTests
    {
    
        [Test]
        public void BasicHeadlessDriverUsage()
        {
            var container = new IoC().GetContainer();
            var headless = container.ResolveKeyed<IWebDriver>(BrowserType.PhantomJS);
            
        }
    }
}
