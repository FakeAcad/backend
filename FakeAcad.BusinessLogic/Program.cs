using FakeAcad.Infrastructure.Extensions;
using FakeAcad.Infrastructure.HttpClients;

var builder = WebApplication.CreateBuilder(args);

builder.AddCorsConfiguration()
    .AddAuthorizationWithSwagger("FakeAcad")
    .AddServices()
    .AddIOHttpClients("http://io-service:80")
    .UseLogger()
    .AddWorkers()
    .ConfigureJson()
    .AddApi();

builder.Services.AddHttpClient<LoginHttpClient>(client =>
{
    client.BaseAddress = new Uri("http://auth:80");
});

var app = builder.Build();

app.ConfigureApplication();
app.Run();
