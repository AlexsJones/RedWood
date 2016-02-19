using Autofac;
using NUnit.Framework;
using RedWood.BootStrap;
using RedWood.Implementation.Process;
using RedWood.Interface.Process;

namespace RedWoodTests
{
    [TestFixture]
    public class ProcessServiceTests
    {
        [SetUp]
        public void Setup()
        {
            var ioc = new IoC();

            ioc.RegistrationDelegate += c => { c.RegisterType<ProcessService>().As<IProcessService>(); };
            _container = ioc.GetContainer();

            _service = _container.Resolve<IProcessService>();
        }

        private IContainer _container;
        private IProcessService _service;

        [Test]
        public void TestStartProcessSync()
        {
            var c = new ProcessConfiguration("Notepad.exe");

            int pid;
            _service.RunProcessASynchronous(c, out pid);

            _service.KillProcess(pid);
        }
    }
}