using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AbakTools.Configuration;
using Newtonsoft.Json;

namespace AbakTools.EnovaApi.Infrastructure.Api
{
    internal class EnovaApiClient : IEnovaApiClient, IDisposable
    {
        private bool _disposed;
        private HttpClient _client;
        private readonly Uri _baseUrl;

        public EnovaApiClient()
        {
            _client = new HttpClient();
            _baseUrl = AppSettings.EnovaApiUrl;
        }

        public async Task<TResult> GetValueAsync<TResult>(string resourceName, Guid objectGuid)
        {
            var uri = CreateUri(resourceName, objectGuid);
            var str = await _client.GetStringAsync(uri);

            return JsonConvert.DeserializeObject<TResult>(str);
        }

        public TResult GetValue<TResult>(string resourceName, Guid objectGuid)
        {
            try
            {
                var uri = CreateUri(resourceName, objectGuid);
                var str = _client.GetStringAsync(uri).Result;

                return JsonConvert.DeserializeObject<TResult>(str);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        private Uri CreateUri(string resourceName, Guid? objectGuid)
        {
            var uri = new UriBuilder(_baseUrl);

            uri.Path = Path.Combine(uri.Path, resourceName, objectGuid?.ToString() ?? string.Empty);

            return uri.Uri;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                if (_client != null)
                {
                    _client.Dispose();
                    _client = null;
                }
            }
        }
    }
}
