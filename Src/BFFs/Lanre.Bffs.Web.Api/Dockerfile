FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Src/BFFs/Lanre.Bffs.Web.Api/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY Src/BFFs/Lanre.Bffs.Web.Api ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Lanre.Bffs.Web.Api.dll"]