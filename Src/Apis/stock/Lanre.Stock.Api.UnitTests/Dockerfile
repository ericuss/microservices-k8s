FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
# COPY ["Src/Directory.Build.props", "Src/Directory.Build.props"]
# COPY ["Src/_configuration", "Src/_configuration"]
COPY ["Src/Apis/stock/Lanre.Stock.Api/Lanre.Stock.Api.csproj", "Src/Apis/stock/Lanre.Stock.Api/"]
COPY ["Src/Apis/stock/Lanre.Stock.Api.UnitTests/Lanre.Stock.Api.UnitTests.csproj", "Src/Apis/stock/Lanre.Stock.Api.UnitTests/"]
RUN dotnet restore "Src/Apis/stock/Lanre.Stock.Api/Lanre.Stock.Api.csproj"
RUN dotnet restore "Src/Apis/stock/Lanre.Stock.Api.UnitTests/Lanre.Stock.Api.UnitTests.csproj"
COPY . .
WORKDIR "/src/Src/Apis/stock/Lanre.Stock.Api.UnitTests/"
RUN dotnet build "Lanre.Stock.Api.UnitTests.csproj" -c Release -o /app


FROM build as unittest
WORKDIR /src/Src/Apis/stock/Lanre.Stock.Api.UnitTests
