using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;

namespace SettingsService.Api.Tests.Fixtures
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
            _httpServer?.Dispose();
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
            var httpClient = new HttpClient(_httpServer, false);
            return httpClient;
        }

        public HttpResponseMessage Get(string url)
        {
            using (var httpClient = CreateServer())
            using (var response = httpClient.GetAsync(BASE_URL + url).Result)
                return response;
        }

        public HttpResponseMessage PostJson(string url, string json)
        {
            using (var httpClient = CreateServer())
            using (var content = new JsonStreamContent(json))
            using (var response = httpClient.PostAsync(BASE_URL + url, content.Payload).Result)
                return response;
        }

        public HttpResponseMessage PutJson(string url, string json)
        {
            using (var httpClient = CreateServer())
            using (var content = new JsonStreamContent(json))
            using (var response = httpClient.PutAsync(BASE_URL + url, content.Payload).Result)
                return response;
        }

        public HttpResponseMessage Delete(string url)
        {
            using (var httpClient = CreateServer())
            using (var response = httpClient.DeleteAsync(BASE_URL + url).Result)
                return response;
        }

        class JsonStreamContent : IDisposable
        {
            private Stream _stream;
            private StreamContent _content;

            public HttpContent Payload => _content;

            public JsonStreamContent(string json)
            {
                _stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
                _content = new StreamContent(_stream);
                _content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            public void Dispose()
            {
                if (_content == null) return;

                _content.Dispose();
                _stream.Dispose();
                _content = null;
                _stream = null;
            }
        }
    }
}