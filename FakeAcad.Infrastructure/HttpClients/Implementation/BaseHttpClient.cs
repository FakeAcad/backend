using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace FakeAcad.Infrastructure.HttpClients
{
    public abstract class BaseHttpClient : IBaseHttpClient
    {
        protected readonly HttpClient _httpClient;
        protected readonly ILogger<BaseHttpClient> _logger;

        protected readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };

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