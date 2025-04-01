# See https://aka.ms/customizecontainer to learn how to customize your debug container 
# and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image with SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files first for dependency resolution
COPY Bootcamp/ReportHub.API/ReportHub.API.csproj ReportHub.API/
COPY Bootcamp/ReportHub.Application/ReportHub.Application.csproj ReportHub.Application/
COPY Bootcamp/ReportHub.Domain/ReportHub.Domain.csproj ReportHub.Domain/
COPY Bootcamp/ReportHub.Infrastructure/ReportHub.Infrastructure.csproj ReportHub.Infrastructure/

# Restore dependencies
RUN dotnet restore "ReportHub.API/ReportHub.API.csproj"

WORKDIR "/src/ReportHub.API"

# Build the project
RUN dotnet build "ReportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ReportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage: runtime container
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportHub.API.dll"]
