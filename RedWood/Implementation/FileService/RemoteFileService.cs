using System;
using System.Net;
using RedWood.Interface.FileService;

namespace RedWood.Implementation.FileService
{
    public class RemoteFileService : IFileService
    {
        public FileServiceFileType FetchType(string path)
        {
            throw new NotImplementedException();
        }

        public bool DoesFileExist(string path)
        {
            try
            {
                var request = WebRequest.Create(path) as HttpWebRequest;
                if (request != null)
                {
                    request.Method = "GET";
                    var response = request.GetResponse() as HttpWebResponse;

                    return response != null && (response.StatusCode == HttpStatusCode.OK);
                }
                else
                {
                    throw new FileException("Could not generate web request from path: " + path);
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DoesDirectoryExist(string path)
        {
            throw new NotImplementedException();
        }

        public string ReadFile(string path)
        {
            throw new NotImplementedException("No Readfile for remote file service currently implemented!");
        }

        public void CopyFile(string apath, string bpath)
        {
            throw new NotImplementedException();
        }

        public void CopyDirectory(string apath, string bpath)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFile(string path)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDirectory(string path)
        {
            throw new NotImplementedException();
        }
    }
}
