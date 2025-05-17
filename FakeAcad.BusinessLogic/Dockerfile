FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app

COPY FakeAcad.Core /app/FakeAcad.Core
COPY FakeAcad.Infrastructure /app/FakeAcad.Infrastructure
COPY FakeAcad.BusinessLogic /app/FakeAcad.BusinessLogic

RUN dotnet restore FakeAcad.BusinessLogic/FakeAcad.BusinessLogic.csproj
RUN dotnet publish FakeAcad.BusinessLogic/FakeAcad.BusinessLogic.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

RUN apt-get update \
    && apt-get install -y curl

ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

COPY FakeAcad.BusinessLogic/startup.sh .
CMD ["./startup.sh"]
