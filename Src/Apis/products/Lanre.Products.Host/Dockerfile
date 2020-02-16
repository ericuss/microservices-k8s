FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy csproj and restore as distinct layers
COPY Src/Apis/products/Lanre.Products.Api/*.csproj Lanre.Products.Api/
COPY Src/Apis/products/Lanre.Products.Host/*.csproj Lanre.Products.Host/
RUN dotnet restore "Lanre.Products.Host/Lanre.Products.Host.csproj"

# Copy everything else and build
COPY Src/Apis/products/Lanre.Products.Host Lanre.Products.Host/
COPY Src/Apis/products/Lanre.Products.Api Lanre.Products.Api/
RUN dotnet publish -c Release -o out Lanre.Products.Host/
COPY Src/Apis/products/Lanre.Products.Host/cert.pfx out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Lanre.Products.Host.dll"]