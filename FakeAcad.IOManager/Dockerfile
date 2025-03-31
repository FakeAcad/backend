FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

COPY ../FakeAcad.Core /app/FakeAcad.Core
COPY ../FakeAcad.Infrastructure /app/FakeAcad.Infrastructure
COPY . /app/FakeAcad.IOManager

WORKDIR /app

RUN dotnet restore FakeAcad.IOManager/FakeAcad.IOManager.csproj
RUN dotnet publish FakeAcad.IOManager/FakeAcad.IOManager.csproj -c Development -o out
