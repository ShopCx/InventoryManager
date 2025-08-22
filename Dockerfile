FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InventoryManager.csproj", "."]
RUN dotnet restore "./InventoryManager.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "InventoryManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InventoryManager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV CONNECTION_STRING="Server=localhost;Database=ShopCx;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True"

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:80/health || exit 1

ENTRYPOINT ["dotnet", "InventoryManager.dll"]
