FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["Src/Apis/stock/Lanre.Stock.Api/Lanre.Stock.Api.csproj", "Src/Apis/stock/Lanre.Stock.Api/"]
COPY ["Src/Apis/stock/Lanre.Stock.Api.IntegrationTests/Lanre.Stock.Api.IntegrationTests.csproj", "Src/Apis/stock/Lanre.Stock.Api.IntegrationTests/"]
RUN dotnet restore "Src/Apis/stock/Lanre.Stock.Api/Lanre.Stock.Api.csproj"
RUN dotnet restore "Src/Apis/stock/Lanre.Stock.Api.IntegrationTests/Lanre.Stock.Api.IntegrationTests.csproj"
COPY . .
WORKDIR "/src/Src/Apis/stock/Lanre.Stock.Api.IntegrationTests/"
RUN dotnet build "Lanre.Stock.Api.IntegrationTests.csproj" -c Release -o /app


FROM build as integrationtests
WORKDIR /src/Src/Apis/stock/Lanre.Stock.Api.IntegrationTests
