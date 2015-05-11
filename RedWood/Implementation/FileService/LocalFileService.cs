using System;
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

        public string ReadFile(string url)
        {
            using (StreamReader sr = new StreamReader(url))
            {
                String line = sr.ReadToEnd();
                return line;
            }
        }
    }
}
