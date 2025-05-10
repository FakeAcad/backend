using FakeAcad.Infrastructure.Extensions;
using FakeAcad.Infrastructure.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddRepository()
    .UseLogger()
    .ConfigureJson()
    .AddApi();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>()
    .UseRouting() 
    .UseSerilogRequestLogging()
    .UseAuthorization();

app.MapControllers();
app.Run();
