using System.Net;
using RedWood.Interface.FileService;

namespace RedWood.Implementation.FileService
{
    class RemoteFileService : IFileService
    {
        public bool DoesFileExist(string url)
        {
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                if (request != null)
                {
                    request.Method = "HEAD";
                    var response = request.GetResponse() as HttpWebResponse;
                    // ReSharper disable once PossibleNullReferenceException
                    return (response.StatusCode == HttpStatusCode.OK);
                }
                else
                {
                    throw new FileException("Could not generate web request from url: " + url);
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
