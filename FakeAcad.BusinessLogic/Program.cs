using FakeAcad.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddCorsConfiguration()
    .AddAuthorizationWithSwagger("FakeAcad")
    .AddServices()
    .AddHttpClients("http://io-service:80")
    .UseLogger()
    .AddWorkers()
    .AddApi();

var app = builder.Build();

app.ConfigureApplication();
app.Run();
