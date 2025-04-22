using Microsoft.Extensions.Logging;

namespace FakeAcad.Infrastructure.HttpClients
{
    public abstract class BaseHttpClient : IBaseHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BaseHttpClient> _logger;

        protected BaseHttpClient(HttpClient httpClient, ILogger<BaseHttpClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending HTTP request");
                throw;
            }
        }
    }
}