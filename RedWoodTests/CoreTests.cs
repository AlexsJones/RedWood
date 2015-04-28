using System.Reflection;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using NSubstitute;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using RedWood;
using RedWood.BootStrap;
using RedWood.Interface.Driver;
using RedWood.Pages.Implementation.Page;
using IContainer = Autofac.IContainer;

namespace RedWoodTests
{
    [TestFixture]
    public class CoreTests
    {
        [Test]
        public void TestContainerFetch()
        {
            IContainer container = new IoC().GetContainer();
            container.Should().NotBeNull();
        }

        public interface ITestInterface
        {
             int GetFoo();
        };
        public class TestClass : ITestInterface
        {
            public int GetFoo()
            {
                return 1;
            }
        };
        
        [Test]
        public void TestContainerRegister()
        {
            IContainer container = new IoC().GetContainer();
            var builder = new ContainerBuilder();         
            var instance = NSubstitute.Substitute.For<ITestInterface>();
            instance.GetFoo().Returns(5);
            builder.RegisterInstance(instance).As<ITestInterface>();
            builder.Update(container);
            container.Resolve<ITestInterface>().GetFoo().Should().Be(5);
        }

        public class TestPage:Page 
        {
            public TestPage(IWebDriver driver) : base(driver,"test")
            {
            }
        }
        [Test]
        public void TestPageConfiguration()
        {
            var builder = new IoC().GetContainer();
            IWebDriver driver = builder.ResolveKeyed<IWebDriver>(BrowserType.PhantomJs);
            Page r = PageConfiguration.GetPage(Assembly.GetExecutingAssembly().GetName().Name,
                "TestPage", driver);

            r.Url.Should().Be("test");
        }

    }
}
