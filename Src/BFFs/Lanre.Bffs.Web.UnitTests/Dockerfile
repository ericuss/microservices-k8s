FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-stretch AS build
WORKDIR /src
# COPY ["Src/Directory.Build.props", "Src/Directory.Build.props"]
# COPY ["Src/_configuration", "Src/_configuration"]
COPY ["Src/Apis/BFFs/Lanre.BFFs.Web.Api/Lanre.BFFs.Web.Api.csproj", "Src/Apis/BFFs/Lanre.BFFs.Web.Api/"]
COPY ["Src/Apis/BFFs/Lanre.BFFs.Web.Api.UnitTests/Lanre.BFFs.Web.Api.UnitTests.csproj", "Src/Apis/BFFs/Lanre.BFFs.Web.Api.UnitTests/"]
RUN dotnet restore "Src/Apis/BFFs/Lanre.BFFs.Web.Api/Lanre.BFFs.Web.Api.csproj"
RUN dotnet restore "Src/Apis/BFFs/Lanre.BFFs.Web.Api.UnitTests/Lanre.BFFs.Web.Api.UnitTests.csproj"
COPY . .
WORKDIR "/src/Src/Apis/BFFs/Lanre.BFFs.Web.Api.UnitTests/"
RUN dotnet build "Lanre.BFFs.Web.Api.UnitTests.csproj" -c Release -o /app


FROM build as unittest
WORKDIR /src/Src/Apis/BFFs/Lanre.BFFs.Web.Api.UnitTests
