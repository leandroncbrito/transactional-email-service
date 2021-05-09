using System;
using Microsoft.AspNetCore.Http;

namespace TransactionalEmail.Consumer.Helpers
{
    public static class UrlHelper
    {
        public static string Generate(HttpRequest request, string path, string parameters = "")
        {
            var urlBuilder = new UriBuilder()
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Path = path,
                Query = parameters
            };

            if (request.Host.Port != null)
            {
                urlBuilder.Port = (int)request.Host.Port;
            }

            return urlBuilder.Uri.ToString();
        }
    }
}
