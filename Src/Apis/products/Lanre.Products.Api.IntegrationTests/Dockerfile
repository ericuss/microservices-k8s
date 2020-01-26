FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["Src/Apis/products/Lanre.Products.Api/Lanre.Products.Api.csproj", "Src/Apis/products/Lanre.Products.Api/"]
COPY ["Src/Apis/products/Lanre.Products.Api.IntegrationTests/Lanre.Products.Api.IntegrationTests.csproj", "Src/Apis/products/Lanre.Products.Api.IntegrationTests/"]
RUN dotnet restore "Src/Apis/products/Lanre.Products.Api/Lanre.Products.Api.csproj"
RUN dotnet restore "Src/Apis/products/Lanre.Products.Api.IntegrationTests/Lanre.Products.Api.IntegrationTests.csproj"
COPY . .
WORKDIR "/src/Src/Apis/products/Lanre.Products.Api.IntegrationTests/"
RUN dotnet build "Lanre.Products.Api.IntegrationTests.csproj" -c Release -o /app


FROM build as integrationtests
WORKDIR /src/Src/Apis/products/Lanre.Products.Api.IntegrationTests
