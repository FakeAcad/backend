FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app

COPY FakeAcad.Core /app/FakeAcad.Core
COPY FakeAcad.Infrastructure /app/FakeAcad.Infrastructure
COPY FakeAcad.AuthService /app/FakeAcad.AuthService

RUN dotnet restore FakeAcad.AuthService/FakeAcad.AuthService.csproj
RUN dotnet publish FakeAcad.AuthService/FakeAcad.AuthService.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

ENTRYPOINT ["dotnet", "FakeAcad.AuthService.dll"]