using System;
using Autofac;
using NUnit.Framework;
using OpenQA.Selenium;
using RedWood;
using RedWood.BootStrap;
using RedWood.Interface.Driver;
using RedWood.Pages.Implementation.Page;

namespace RedWoodIntegrationTests
{
    [TestFixture]
    public class CoreIntegrationTests
    {
        [Test]
        public void BasicHeadlessDriverUsage()
        {
            var container = new IoC().GetContainer();
            var headless = container.ResolveKeyed<IWebDriver>(BrowserType.Chrome);
            headless.Navigate().GoToUrl("http://www.google.com");
            Assert.AreEqual(headless.Title,"Google");
            headless.Quit();
        }
    }
}
