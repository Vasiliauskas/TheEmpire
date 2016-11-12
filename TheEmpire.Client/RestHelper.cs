using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using System.IO;
using Newtonsoft.Json;

namespace TheEmpire.Client
{
    public static class RestHelper
    {
        
        public static WebResponse SendPost(Uri uri, string serializedData)
        {
            var request = WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/json;charset=utf-8";

            var encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(serializedData);
            request.ContentLength = bytes.Length;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }

            return request.GetResponse();
        }
    }
}
