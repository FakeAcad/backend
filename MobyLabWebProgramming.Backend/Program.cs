using MobyLabWebProgramming.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddCorsConfiguration()
    .AddRepository()
    .AddAuthorizationWithSwagger("FakeAcad")
    .AddServices()
    .UseLogger()
    .AddWorkers()
    .AddApi();

var app = builder.Build();

app.ConfigureApplication();
app.Run();
