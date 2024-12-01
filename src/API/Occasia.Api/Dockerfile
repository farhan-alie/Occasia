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
COPY ["Directory.Build.props", "."]
COPY ["src/API/Occasia.Api/Occasia.Api.csproj", "src/API/Occasia.Api/"]
COPY ["src/Common/Occasia.Common.Application/Occasia.Common.Application.csproj", "src/Common/Occasia.Common.Application/"]
COPY ["src/Common/Occasia.Common.Domain/Occasia.Common.Domain.csproj", "src/Common/Occasia.Common.Domain/"]
COPY ["src/Common/Occasia.Common.Infastructure/Occasia.Common.Infastructure.csproj", "src/Common/Occasia.Common.Infastructure/"]
COPY ["src/Modules/Events/Occasia.Modules.Events.Presentation/Occasia.Modules.Events.Presentation.csproj", "src/Modules/Events/Occasia.Modules.Events.Presentation/"]
COPY ["src/Modules/Events/Occasia.Modules.Events.Infrastructure/Occasia.Modules.Events.Infrastructure.csproj", "src/Modules/Events/Occasia.Modules.Events.Infrastructure/"]
COPY ["src/Modules/Events/Occasia.Modules.Events.Application/Occasia.Modules.Events.Application.csproj", "src/Modules/Events/Occasia.Modules.Events.Application/"]
COPY ["src/Modules/Events/Occasia.Modules.Events.Domain/Occasia.Modules.Events.Domain.csproj", "src/Modules/Events/Occasia.Modules.Events.Domain/"]
RUN dotnet restore "./src/API/Occasia.Api/Occasia.Api.csproj"
COPY . .
WORKDIR "/src/src/API/Occasia.Api"
RUN dotnet build "./Occasia.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Occasia.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Occasia.Api.dll"]