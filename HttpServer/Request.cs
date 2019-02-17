using System;
using System.Collections.Generic;
using System.Text;

namespace HttpServer
{
    public class Request
    {
        public string Type { get; set; }

        public string Url { get; set; }

        public string Host { get; set; }
        
        private Request(string type, string url, string host)
        {
            Type = type;
            Url = url;
            Host = host;
        }

        public static Request GetRequest(string request)
        {
            if (string.IsNullOrEmpty(request))
            {
                return null;
            }

            var tokens = request.Split(' ');
            var type = tokens[0];
            var url = tokens[1];
            var host = tokens[4];
            return new Request(type, url, host);
        }
    }
}
