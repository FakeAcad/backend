using System.Net.Http.Json;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Responses;
using Microsoft.Extensions.Logging;

namespace FakeAcad.Infrastructure.HttpClients
{
    public class ComplaintHttpClient : BaseHttpClient
    {
        private readonly string _baseUrl = "api/Complaint";

        public ComplaintHttpClient(HttpClient httpClient, ILogger<ComplaintHttpClient> logger)
            : base(httpClient, logger)
        {
        }

        public async Task<RequestResponse<ComplaintDTO>> GetByIdAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetById/{id}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<ComplaintDTO>>();
            return result ?? RequestResponse<ComplaintDTO>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse<ComplaintDTO>> GetByNameAsync(string name)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetById/{name}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<ComplaintDTO>>();
            return result ?? RequestResponse<ComplaintDTO>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse> AddComplaintAsync(ComplaintAddDTO complaint)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/Add");
            request.Content = JsonContent.Create(complaint);
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse>();
            return result ?? RequestResponse.FromError(CommonErrors.FailedToDeserialize);
        }
    }
}