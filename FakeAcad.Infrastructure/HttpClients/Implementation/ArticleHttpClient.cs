using System.Net.Http.Json;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Responses;
using Microsoft.Extensions.Logging;

namespace FakeAcad.Infrastructure.HttpClients
{
    public class ArticleHttpClient : BaseHttpClient
    {
        private readonly string _baseUrl = "api/Article";

        public ArticleHttpClient(HttpClient httpClient, ILogger<ArticleHttpClient> logger)
            : base(httpClient, logger)
        {
        }

        public async Task<RequestResponse<ArticleDTO>> GetByIdAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetById/{id}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<ArticleDTO>>();
            return result ?? RequestResponse<ArticleDTO>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse<ArticleDTO>> GetByTitleAsync(string title)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetByTitle/{title}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<ArticleDTO>>();
            return result ?? RequestResponse<ArticleDTO>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse<ICollection<ArticleDTO>>> GetByUniversityAsync(string university)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetByUniv/{university}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<ICollection<ArticleDTO>>>();
            return result ?? RequestResponse<ICollection<ArticleDTO>>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse<ICollection<ArticleDTO>>> GetByProfessorAsync(string firstName, string lastName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetByProf/{firstName},{lastName}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<ICollection<ArticleDTO>>>();
            return result ?? RequestResponse<ICollection<ArticleDTO>>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse> AddArticleAsync(ArticleAddDTO article)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/Add");
            request.Content = JsonContent.Create(article);
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse>();
            return result ?? RequestResponse.FromError(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse> DeleteArticleAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/Delete/{id}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse>();
            return result ?? RequestResponse.FromError(CommonErrors.FailedToDeserialize);
        }
    }
}