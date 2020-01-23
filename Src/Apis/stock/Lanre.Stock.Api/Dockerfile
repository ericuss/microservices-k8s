FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Src/Apis/stock/Lanre.Stock.Api/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY Src/Apis/stock/Lanre.Stock.Api ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Lanre.Stock.Api.dll"]