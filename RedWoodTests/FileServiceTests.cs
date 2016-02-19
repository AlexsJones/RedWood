﻿using Autofac;
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
        private readonly string _targetRemoteFolderPath = @"..\..\Properties";
        private readonly string _targetRemoteFolderCopy = @"Properties";
        private readonly string _targetRemoteFilePath = @"..\..\Properties\AssemblyInfo.cs";

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
    }
}