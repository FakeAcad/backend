using FakeAcad.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddCorsConfiguration()
    .AddRepository()
    .AddServices()
    .UseLogger()
    .AddApi();

var app = builder.Build();

app.ConfigureApplication();
app.Run();
