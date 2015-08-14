using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using RedWood.BootStrap;
using RedWood.Implementation.FileService;
using RedWood.Interface.FileService;

namespace RedWoodTests
{
    [TestFixture]
    public class FileServiceTests
    {
        private IContainer _container;

        private IFileService _service;

        private string _targetRemoteFolderPath = @"..\..\Properties";

        private readonly string _targetRemoteFolderCopy = @"Properties";

        private string _targetRemoteFilePath = @"..\..\Properties\AssemblyInfo.cs";

        [SetUp]
        public void Setup()
        {
            var ioc = new IoC();

            ioc.RegistrationDelegate += (c) =>
            {
                c.RegisterType<WindowsFileService>().As<IFileService>();
            };
            _container = ioc.GetContainer();

            _service = _container.Resolve<IFileService>();

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
            _service.FetchType(_targetRemoteFilePath).Should().Be(FileServiceFileType.File);

            _service.FetchType(_targetRemoteFolderPath).Should().Be(FileServiceFileType.Directory);

        }

        [Test]
        public void TestWindowsFileSystemCopyToLocalDirectory()
        {
            _service.CopyDirectory(_targetRemoteFolderPath, "RemoteBuildFolder", true);

            _service.DoesDirectoryExist("RemoteBuildFolder").Should().BeTrue();
        }

        [Test]
        public void TestWindowsFileSystemCopyToLocalFile()
        {
            _service.CopyFile(_targetRemoteFilePath, "Somefile.txt");

            _service.DoesFileExist("Somefile.txt").Should().BeTrue();
        }

        [Test]
        public void TestWindowsFileSystemDeleteRemoteFolder()
        {
            _service.CopyDirectory(_targetRemoteFolderPath, _targetRemoteFolderCopy, true);
         
            _service.DoesDirectoryExist(_targetRemoteFolderCopy);

            _service.DeleteDirectory(_targetRemoteFolderCopy).Should().BeTrue();
        }

    }
}
