using System.IO;
using RedWood.Interface.FileService;

namespace RedWood.Implementation.FileService
{
    class LocalFileService : IFileService
    {
        public bool DoesFileExist(string url)
        {
            return File.Exists(url);
        }
    }
}
