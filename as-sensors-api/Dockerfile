# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["as-sensors-api/as-sensors-api.csproj", "as-sensors-api/"]
COPY ["as-sensors-application/as-sensors-application.csproj", "as-sensors-application/"]
COPY ["as-sensors-domain/as-sensors-domain.csproj", "as-sensors-domain/"]
COPY ["as-sensors-infra/as-sensors-infra.csproj", "as-sensors-infra/"]
RUN dotnet restore "./as-sensors-api/as-sensors-api.csproj"
COPY . .
WORKDIR "/src/as-sensors-api"
RUN dotnet build "./as-sensors-api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./as-sensors-api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "as-sensors-api.dll"]