using FakeAcad.Infrastructure.HttpClients;
using Microsoft.Extensions.DependencyInjection;

namespace FakeAcad.Infrastructure.Extensions
{
    public static class HttpClientServiceExtensions
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, string baseUrl)
        {
            services.AddHttpClient<ArticleHttpClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            services.AddHttpClient<UserHttpClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            services.AddHttpClient<ComplaintHttpClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            services.AddHttpClient<ProfessorHttpClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            services.AddHttpClient<UniversityHttpClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            return services;
        }
    }
}