using Autofac;
using FluentAssertions;
using NUnit.Framework;
using RedWood.BootStrap;
using RedWood.Implementation.FileService;
using RedWood.Interface.FileService;
using System.IO;

namespace RedWoodTests
{
    [TestFixture]
    public class FileServiceTests
    {
        [SetUp]
        public void Setup()
        {
            var ioc = new IoC();

            ioc.RegistrationDelegate += c => { c.RegisterType<WindowsFileService>().As<IFileService>(); };
            _container = ioc.GetContainer();

            _service = _container.Resolve<IFileService>();
        }

        private IContainer _container;
        private IFileService _service;
        private readonly string _targetRemoteFolderPath = TestContext.CurrentContext.TestDirectory + @"\..\..\obj";
        private readonly string _targetRemoteFolderCopy = TestContext.CurrentContext.TestDirectory + @"\obj";
        private readonly string _targetRemoteFilePath = TestContext.CurrentContext.TestDirectory + 
            @"\..\..\CoreTests.cs";

        [Test]
        public void TestWindowsFileSystemCopyToLocalDirectory()
        {
            _service.CopyDirectory(_targetRemoteFolderPath, TestContext.CurrentContext.TestDirectory + 
                @"\RemoteBuildFolder", true);

            _service.DoesDirectoryExist(TestContext.CurrentContext.TestDirectory + 
                @"\RemoteBuildFolder").Should().BeTrue();
        }

        [Test]
        public void TestWindowsFileSystemCopyToLocalFile()
        {
            _service.CopyFile(_targetRemoteFilePath, TestContext.CurrentContext.TestDirectory +
                @"\Somefile.txt");

            _service.DoesFileExist(TestContext.CurrentContext.TestDirectory +
                @"\Somefile.txt").Should().BeTrue();
        }

        [Test]
        public void TestWindowsFileSystemDeleteRemoteFolder()
        {
            _service.CopyDirectory(_targetRemoteFolderPath, _targetRemoteFolderCopy, true);

            _service.DoesDirectoryExist(_targetRemoteFolderCopy);

            _service.DeleteDirectory(_targetRemoteFolderCopy).Should().BeTrue();
        }

        [Test]
        public void TestWindowsFileSystemDirExists()
        {
            _service.DoesDirectoryExist(_targetRemoteFolderPath).Should().BeTrue();
        }

        [Test]
        public void TestWindowsFileSystemFileExists()
        {
            _service.DoesFileExist(_targetRemoteFilePath).Should().BeTrue();
        }

        [Test]
        public void TestWindowsFileSystemTypes()
        {

            string solution_dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            _service.FetchType(_targetRemoteFilePath).Should().Be(FileServiceFileType.File);

            _service.FetchType(_targetRemoteFolderPath).Should().Be(FileServiceFileType.Directory);
        }
    }
}