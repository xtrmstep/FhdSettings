using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Xunit;

namespace FhdSettings.Api.Tests.Fixtures
{
    public class HttpServerFixture : IDisposable
    {
        private const string BASE_URL = "http://dummyurl/";
        private readonly HttpServer _httpServer;

        public HttpServerFixture()
        {
            var config = new HttpConfiguration();
            WebApiApplication.Configure(config);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.EnsureInitialized();
            _httpServer = new HttpServer(config);
        }

        public void Dispose()
        {
            if (_httpServer != null)
            {
                _httpServer.Dispose();
            }
        }

        public HttpRequestMessage CreateRequest(string url, string mthv, HttpMethod method)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(BASE_URL + url)
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            request.Method = method;
            return request;
        }

        public HttpRequestMessage CreateRequest<T>(string url, string mthv, HttpMethod method, T content, MediaTypeFormatter formatter) where T : class
        {
            var request = CreateRequest(url, mthv, method);
            request.Content = new ObjectContent<T>(content, formatter);

            return request;
        }

        public HttpClient CreateServer()
        {
            return new HttpClient(_httpServer);
        }

        public HttpResponseMessage GetJson(string url)
        {
            using (var request = CreateRequest(url, "text/plain", HttpMethod.Get))
            {
                using (var response = CreateServer().SendAsync(request).Result)
                {
                    return response;
                }
            }
        }

        public HttpResponseMessage GetJson(string url, string json)
        {
            using (var request = CreateRequest(url, "text/plain", HttpMethod.Get))
            {
                request.Content = new ObjectContent(typeof (string), json, new JsonMediaTypeFormatter());
                using (var response = CreateServer().SendAsync(request).Result)
                {
                    return response;
                }
            }
        }

        public HttpResponseMessage PostJson(string url, string json)
        {
            using (var request = CreateRequest(url, "text/plain", HttpMethod.Post))
            {
                request.Content = new ObjectContent(typeof (string), json, new JsonMediaTypeFormatter());
                using (var response = CreateServer().SendAsync(request).Result)
                {
                    return response;
                }
            }
        }

        public HttpResponseMessage PutJson(string url, string json)
        {
            using (var request = CreateRequest(url, "text/plain", HttpMethod.Put))
            {
                request.Content = new ObjectContent(typeof (string), json, new JsonMediaTypeFormatter());
                using (var response = CreateServer().SendAsync(request).Result)
                {
                    return response;
                }
            }
        }

        public HttpResponseMessage DeleteJson(string url, string json)
        {
            using (var request = CreateRequest(url, "text/plain", HttpMethod.Delete))
            {
                request.Content = new ObjectContent(typeof (string), json, new JsonMediaTypeFormatter());
                using (var response = CreateServer().SendAsync(request).Result)
                {
                    return response;
                }
            }
        }
    }
}