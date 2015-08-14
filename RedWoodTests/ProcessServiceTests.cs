using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using NUnit.Framework;
using RedWood.BootStrap;
using RedWood.Implementation.FileService;
using RedWood.Implementation.Process;
using RedWood.Interface.FileService;
using RedWood.Interface.Process;

namespace RedWoodTests
{

    [TestFixture]
    public class ProcessServiceTests
    {
        private IContainer _container;

        private IProcessService _service;


        [SetUp]
        public void Setup()
        {
            var ioc = new IoC();

            ioc.RegistrationDelegate += (c) =>
            {
                c.RegisterType<ProcessService>().As<IProcessService>();
            };
            _container = ioc.GetContainer();

            _service = _container.Resolve<IProcessService>();

        }

        [Test]
        public void TestStartProcessAsync()
        {
            ProcessConfiguration c = new ProcessConfiguration("Notepad.exe");

            int pid;
            _service.RunProcessASynchronous(c, out pid);

           _service.KillProcess(pid);
        }

    }

}
