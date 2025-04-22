using FakeAcad.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddRepository()
    .UseLogger()
    .AddApi();

var app = builder.Build();

app.ConfigureApplication();
app.Run();
