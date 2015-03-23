using System;
using System.ComponentModel;
using Autofac;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public void TestContainerRegister()
        {
            IContainer container = new IoC().GetContainer();
            
        }
    }
}
