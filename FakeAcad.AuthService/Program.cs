using FakeAcad.Infrastructure.Configurations;
using FakeAcad.Infrastructure.Extensions;
using FakeAcad.Infrastructure.HttpClients;
using FakeAcad.Infrastructure.Services.Implementations;
using FakeAcad.Infrastructure.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.AddCorsConfiguration()
    .AddAuthorizationWithSwagger("FakeAcad")
    .UseLogger()
    .ConfigureJson()
    .AddApi();

builder.Services.AddHttpClient<UserHttpClient>(client => { client.BaseAddress = new Uri("http://io-service:80"); });

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(nameof(JwtConfiguration)));
builder.Services.Configure<MailConfiguration>(builder.Configuration.GetSection(nameof(MailConfiguration)));

builder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<ILoginService, LoginService>()
    .AddScoped<IMailService, MailService>();


var app = builder.Build();

app.ConfigureApplication();
app.Run();
