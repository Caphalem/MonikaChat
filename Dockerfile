#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
USER app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MonikaChat.Server/MonikaChat.Server.csproj", "MonikaChat.Server/"]
COPY ["MonikaChat.Client/MonikaChat.Client.csproj", "MonikaChat.Client/"]
COPY ["MonikaChat.Shared/MonikaChat.Shared.csproj", "MonikaChat.Shared/"]
RUN dotnet restore "./MonikaChat.Server/MonikaChat.Server.csproj"
COPY . .
WORKDIR "/src/MonikaChat.Server"
RUN dotnet build "./MonikaChat.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MonikaChat.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonikaChat.Server.dll"]