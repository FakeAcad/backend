namespace FakeAcad.Infrastructure.HttpClients
{
    public interface IBaseHttpClient
    {
        Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request);
    }
}