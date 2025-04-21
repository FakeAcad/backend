FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app

COPY FakeAcad.Core /app/FakeAcad.Core
COPY FakeAcad.Infrastructure /app/FakeAcad.Infrastructure
COPY FakeAcad.IOManager /app/FakeAcad.IOManager

RUN dotnet restore FakeAcad.IOManager/FakeAcad.IOManager.csproj
RUN dotnet publish FakeAcad.IOManager/FakeAcad.IOManager.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80

ENTRYPOINT ["dotnet", "FakeAcad.IOManager.dll"]