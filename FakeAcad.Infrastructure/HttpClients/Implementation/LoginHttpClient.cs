using System.Net.Http.Json;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Responses;
using Microsoft.Extensions.Logging;

namespace FakeAcad.Infrastructure.HttpClients
{
    public class LoginHttpClient : BaseHttpClient
    {
        private readonly string _baseUrl = "api/Authorization";

        public LoginHttpClient(HttpClient httpClient, ILogger<LoginHttpClient> logger)
            : base(httpClient, logger)
        {
        }

        public async Task<RequestResponse<LoginResponseDTO>> LoginAsync(LoginDTO login)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/Login");
            request.Content = JsonContent.Create(login);
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<LoginResponseDTO>>(_jsonSerializerOptions);
            return result ?? RequestResponse<LoginResponseDTO>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }
    }
}