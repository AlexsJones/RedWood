using NUnit.Framework;
using OpenQA.Selenium.Firefox;

namespace RedWoodIntegrationTests
{
    [TestFixture]
    public class CoreIntegrationTests
    {
        [Test]
        public void BasicHeadlessDriverUsage()
        {
            var headless = new FirefoxDriver(
                new FirefoxBinary("C:\\Program Files (x86)\\Mozilla Firefox\\firefox.exe"), new FirefoxProfile());
            headless.Navigate().GoToUrl("http://www.google.com");
            Assert.AreEqual(headless.Title, "Google");
            headless.Quit();
        }
    }
}