﻿using System;
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
                    request.Method = "GET";
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

        public string ReadFile(string url)
        {
            throw new NotImplementedException("No Readfile for remote file service currently implemented!");
        }
    }
}
