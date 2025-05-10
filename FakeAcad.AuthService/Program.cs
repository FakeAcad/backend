using FakeAcad.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddCorsConfiguration()
    .AddServices()
    .UseLogger()
    .ConfigureJson()
    .AddApi();

var app = builder.Build();

app.ConfigureApplication();
app.Run();
