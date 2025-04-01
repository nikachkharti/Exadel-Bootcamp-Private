# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image with SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only project files first for dependency resolution
COPY ReportHub.API/ReportHub.API.csproj ReportHub.API/
COPY ReportHub.Application/ReportHub.Application.csproj ReportHub.Application/
COPY ReportHub.Domain/ReportHub.Domain.csproj ReportHub.Domain/
COPY ReportHub.Infrastructure/ReportHub.Infrastructure.csproj ReportHub.Infrastructure/

# Restore dependencies
RUN dotnet restore "ReportHub.API/ReportHub.API.csproj"

# Copy the entire source code
COPY . .

WORKDIR "/src/ReportHub.API"

# Build the application
RUN dotnet build "ReportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ReportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime container
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportHub.API.dll"]
