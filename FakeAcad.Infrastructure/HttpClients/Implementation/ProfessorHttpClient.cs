using System.Net.Http.Json;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Responses;
using Microsoft.Extensions.Logging;

namespace FakeAcad.Infrastructure.HttpClients
{
    public class ProfessorHttpClient : BaseHttpClient
    {
        private readonly string _baseUrl = "api/Professor";

        public ProfessorHttpClient(HttpClient httpClient, ILogger<ProfessorHttpClient> logger)
            : base(httpClient, logger)
        {
        }

        public async Task<RequestResponse<ProfessorDTO>> GetByIdAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetById/{id}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<ProfessorDTO>>(_jsonSerializerOptions);
            return result ?? RequestResponse<ProfessorDTO>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse<ProfessorDTO>> GetByNameAsync(string firstName, string lastName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetByName/{firstName},{lastName}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<ProfessorDTO>>(_jsonSerializerOptions);
            return result ?? RequestResponse<ProfessorDTO>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse<ICollection<ProfessorDTO>>> GetByArticleAsync(string article)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetByArticle/{article}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<ICollection<ProfessorDTO>>>(_jsonSerializerOptions);
            return result ?? RequestResponse<ICollection<ProfessorDTO>>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse<ICollection<ProfessorDTO>>> GetByUniversityAsync(string university)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetByUniv/{university}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<ICollection<ProfessorDTO>>>(_jsonSerializerOptions);
            return result ?? RequestResponse<ICollection<ProfessorDTO>>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse> AddProfessorAsync(ProfessorAddDTO professor)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/Add");
            request.Content = JsonContent.Create(professor);
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse>(_jsonSerializerOptions);
            return result ?? RequestResponse.FromError(CommonErrors.FailedToDeserialize);
        }
    }
}