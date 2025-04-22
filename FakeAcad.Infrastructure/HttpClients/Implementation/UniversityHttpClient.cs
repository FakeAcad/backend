using System.Net.Http.Json;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Responses;
using Microsoft.Extensions.Logging;

namespace FakeAcad.Infrastructure.HttpClients
{
    public class UniversityHttpClient : BaseHttpClient
    {
        private readonly string _baseUrl = "api/University";

        public UniversityHttpClient(HttpClient httpClient, ILogger<UniversityHttpClient> logger)
            : base(httpClient, logger)
        {
        }

        public async Task<RequestResponse<UniversityDTO>> GetByIdAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetById/{id}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<UniversityDTO>>();
            return result ?? RequestResponse<UniversityDTO>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse<UniversityDTO>> GetByNameAsync(string name)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetByName/{name}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<UniversityDTO>>();
            return result ?? RequestResponse<UniversityDTO>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse<ICollection<UniversityDTO>>> GetByArticleAsync(string article)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetByArticle/{article}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<ICollection<UniversityDTO>>>();
            return result ?? RequestResponse<ICollection<UniversityDTO>>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse<ICollection<UniversityDTO>>> GetByProfessorAsync(string firstName, string lastName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetByProf/{firstName},{lastName}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<ICollection<UniversityDTO>>>();
            return result ?? RequestResponse<ICollection<UniversityDTO>>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse> AddUniversityAsync(UniversityAddDTO university)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/Add");
            request.Content = JsonContent.Create(university);
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse>();
            return result ?? RequestResponse.FromError(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse> AddProfessorToUniversityAsync(Guid universityId, Guid professorId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/AddProfToUniversity/{universityId}/{professorId}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse>();
            return result ?? RequestResponse.FromError(CommonErrors.FailedToDeserialize);
        }
    }
}