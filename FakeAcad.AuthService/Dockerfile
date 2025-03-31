FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

COPY ../FakeAcad.Core /app/FakeAcad.Core
COPY ../FakeAcad.Infrastructure /app/FakeAcad.Infrastructure
COPY . /app/FakeAcad.AuthService

WORKDIR /app

RUN dotnet restore FakeAcad.AuthService/FakeAcad.AuthService.csproj
RUN dotnet publish FakeAcad.AuthService/FakeAcad.AuthService.csproj -c Development -o out
