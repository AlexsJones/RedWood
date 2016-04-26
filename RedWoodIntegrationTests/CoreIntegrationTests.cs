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
            var headless = new OpenQA.Selenium.PhantomJS.PhantomJSDriver(phantomJSDriverServerDirectory: TestContext.CurrentContext.TestDirectory);
            headless.Navigate().GoToUrl("http://www.google.com");
            Assert.AreEqual(headless.Title, "Google");
            headless.Quit();
        }
    }
}