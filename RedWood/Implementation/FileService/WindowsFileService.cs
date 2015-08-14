using System.IO;
using System.Linq;
using RedWood.Interface.FileService;

namespace RedWood.Implementation.FileService
{
    public class WindowsFileService : IFileService
    {
        public FileServiceFileType FetchType(string path)
        {
            var attr = File.GetAttributes(path);

            return (attr & FileAttributes.Directory) == FileAttributes.Directory
                ? FileServiceFileType.Directory
                : FileServiceFileType.File;
        }

        public bool DoesFileExist(string path)
        {
            return File.Exists(path);
        }

        public bool DoesDirectoryExist(string path)
        {
            return Directory.Exists(path);
        }

        public string ReadFile(string path)
        {
            using (var sr = new StreamReader(path))
            {
                var line = sr.ReadToEnd();
                return line;
            }
        }

        public void CopyFile(string apath, string bpath)
        {
            if (FetchType(apath) != FileServiceFileType.File)
            {
                throw new FileException("Cannot copy non file attribute target path");
            }

            File.Copy(apath, bpath, true);
        }

        public void CopyDirectory(string apath, string bpath, bool ignoreHidden)
        {
            if (FetchType(apath) != FileServiceFileType.Directory)
            {
                throw new FileException("Cannot copy non directory attribute target path");
            }

            var dir = new DirectoryInfo(apath);

            var dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new FileException(
                    "Source directory does not exist or could not be found: "
                    + apath);
            }

            if (!Directory.Exists(bpath))
            {
                Directory.CreateDirectory(bpath);
            }

            var files = dir.GetFiles();

            if (ignoreHidden)
            {
                var filtered = files.Select(f => f)
                    .Where(f => (f.Attributes & FileAttributes.Hidden) == 0);

                files = filtered.ToArray();
            }
  
            foreach (var file in files)
            {
                var temppath = Path.Combine(bpath, file.Name);
                file.CopyTo(temppath, true);
            }

            foreach (var subdir in dirs)
            {
                var temppath = Path.Combine(bpath, subdir.Name);
                CopyDirectory(subdir.FullName, temppath, ignoreHidden);
            }
        }

        public bool DeleteFile(string path)
        {
            if (FetchType(path) != FileServiceFileType.File)
            {
                throw new FileException("Cannot copy non directory attribute target path");
            }

            File.Delete(path);

            if (DoesFileExist(path))
            {
                return false;
            }
            return true;
        }

        public bool DeleteDirectory(string path)
        {
            if (FetchType(path) != FileServiceFileType.Directory)
            {
                throw new FileException("Cannot copy non directory attribute target path");
            }

            Directory.Delete(path, true);

            if (DoesDirectoryExist(path))
            {
                return false;
            }
            return true;
        }
    }
}