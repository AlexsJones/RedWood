using System;
using Autofac;
using Autofac.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.PhantomJS;
using RedWood;
using RedWood.BootStrap;
using RedWood.Implementation.FileService;
using RedWood.Interface.Driver;
using RedWood.Interface.FileService;
using RedWood.Pages.Implementation.Page;

namespace RedWoodIntegrationTests
{
    [TestFixture]
    public class CoreIntegrationTests
    {
        [Test]
        public void BasicHeadlessDriverUsage()
        {


            var headless = new FirefoxDriver(new FirefoxBinary("C:\\Program Files (x86)\\Mozilla Firefox\\firefox.exe"),new FirefoxProfile());
            headless.Navigate().GoToUrl("http://www.google.com");
            Assert.AreEqual(headless.Title,"Google");
            headless.Quit();

        }
    }
}
