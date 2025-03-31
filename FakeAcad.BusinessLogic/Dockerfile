FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

COPY ../FakeAcad.Core /app/FakeAcad.Core
COPY ../FakeAcad.Infrastructure /app/FakeAcad.Infrastructure
COPY . /app/FakeAcad.BusinessLogic

WORKDIR /app

RUN dotnet restore FakeAcad.BusinessLogic/FakeAcad.BusinessLogic.csproj
RUN dotnet publish FakeAcad.BusinessLogic/FakeAcad.BusinessLogic.csproj -c Development -o out
