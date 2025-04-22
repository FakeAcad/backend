using System.Net.Http.Json;
using FakeAcad.Core.DataTransferObjects;
using FakeAcad.Core.Errors;
using FakeAcad.Core.Requests;
using FakeAcad.Core.Responses;
using Microsoft.Extensions.Logging;

namespace FakeAcad.Infrastructure.HttpClients
{
    public class UserHttpClient : BaseHttpClient
    {
        private readonly string _baseUrl = "api/User";

        public UserHttpClient(HttpClient httpClient, ILogger<UserHttpClient> logger)
            : base(httpClient, logger)
        {
        }

        public async Task<RequestResponse<UserDTO>> GetByIdAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetById/{id}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<UserDTO>>();
            return result ?? RequestResponse<UserDTO>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse<PagedResponse<UserDTO>>> GetPageAsync(PaginationSearchQueryParams pagination)
        {
            var queryString = $"?Page={pagination.Page}&PageSize={pagination.PageSize}";
            if (!string.IsNullOrEmpty(pagination.Search))
                queryString += $"&Search={pagination.Search}";

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/GetPage{queryString}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse<PagedResponse<UserDTO>>>();
            return result ?? RequestResponse<PagedResponse<UserDTO>>.FromErrorAnyType(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse> AddUserAsync(UserAddDTO user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/Add");
            request.Content = JsonContent.Create(user);
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse>();
            return result ?? RequestResponse.FromError(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse> UpdateUserAsync(UserUpdateDTO user)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUrl}/Update");
            request.Content = JsonContent.Create(user);
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse>();
            return result ?? RequestResponse.FromError(CommonErrors.FailedToDeserialize);
        }

        public async Task<RequestResponse> DeleteUserAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/Delete/{id}");
            var response = await SendRequestAsync(request);
            var result = await response.Content.ReadFromJsonAsync<RequestResponse>();
            return result ?? RequestResponse.FromError(CommonErrors.FailedToDeserialize);
        }
    }
}