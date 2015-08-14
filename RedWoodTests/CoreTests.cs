using System.Reflection;
using Autofac;
using Autofac.Core;
using FluentAssertions;
using NUnit.Framework;
using NSubstitute;
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
using IContainer = Autofac.IContainer;

namespace RedWoodTests
{
    [TestFixture]
    public class CoreTests
    {
        private IContainer _container;

        [SetUp]
        public void Setup()
        {
            var ioc = new IoC();

            ioc.RegistrationDelegate += (e) =>
            {
                e.RegisterType<FirefoxDriver>().Keyed<IWebDriver>(BrowserType.Firefox);

                e.RegisterType<OperaDriver>().Keyed<IWebDriver>(BrowserType.Opera);

                e.RegisterType<InternetExplorerDriver>().Keyed<IWebDriver>(BrowserType.InternetExplorer).
                     WithParameters(new[]
                    {
                    new ResolvedParameter((p,c) =>
                        p.Name == "internetExplorerDriverServerDirectory",
                        (p,c) => ioc.DirProject()),
                    });

                e.RegisterType<ChromeDriver>().Keyed<IWebDriver>(BrowserType.Chrome).WithParameters(
                    new[]
                    {
                    new ResolvedParameter((p,c) =>
                        p.Name == "chromeDriverDirectory",
                        (p,c) => ioc.DirProject()),
                    });

                e.RegisterType<PhantomJSDriver>().
                    Keyed<IWebDriver>(BrowserType.PhantomJs).
                    WithParameters(new[]
                    {
                    new ResolvedParameter((p,c) =>
                        p.Name == "phantomJSDriverServerDirectory",
                        (p,c) => ioc.DirProject()),
                    });
                e.RegisterType<WindowsFileService>().Keyed<IFileService>(FileServiceType.Windows);

                e.RegisterType<RemoteFileService>().Keyed<IFileService>(FileServiceType.Remote);
            };

            _container = ioc.GetContainer();
        }

        [Test]
        public void TestContainerFetch()
        { 
            _container.Should().NotBeNull();
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

            IWebDriver driver = _container.ResolveKeyed<IWebDriver>(BrowserType.PhantomJs);

            Page r = PageConfiguration.GetPage(Assembly.GetExecutingAssembly().GetName().Name,
                "TestPage", driver);

            r.Url.Should().Be("test");
        }

        [Test]
        public void TestFileServiceLocalRemote()
        {
            IFileService fs = _container.ResolveKeyed<IFileService>(FileServiceType.Remote);

            fs.DoesFileExist("http://www.google.com").Should().BeTrue();

            fs.DoesFileExist("RedWood.dll").Should().BeFalse();

            fs = _container.ResolveKeyed<IFileService>(FileServiceType.Windows);

            fs.DoesFileExist("RedWood.dll").Should().BeTrue();
        }
    }
}
