using Autofac;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RedWood;
using RedWood.Interface.Debug;
using IContainer = Autofac.IContainer;

namespace RedWoodTests
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void TestContainerFetch()
        {
            IContainer container = new IoC().GetContainer();
            container.Should().NotBeNull();
            container.IsRegistered<ILogger>().Should().BeTrue();
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
        [TestMethod]
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
    }
}
