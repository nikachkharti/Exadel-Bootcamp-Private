# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ReportHub.API/ReportHub.API.csproj", "ReportHub.API/"]
COPY ["ReportHub.Application/ReportHub.Application.csproj", "ReportHub.Application/"]
COPY ["ReportHub.Domain/ReportHub.Domain.csproj", "ReportHub.Domain/"]
COPY ["ReportHub.Infrastructure/ReportHub.Infrastructure.csproj", "ReportHub.Infrastructure/"]
RUN dotnet restore "./ReportHub.API/ReportHub.API.csproj"
COPY . .
WORKDIR "/src/ReportHub.API"
RUN dotnet build "./ReportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ReportHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportHub.API.dll"]