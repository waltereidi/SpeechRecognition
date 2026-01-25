# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração
# e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# =========================
# Base (runtime + ffmpeg)
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble AS base

USER root

# Instala ffmpeg
RUN apt-get update \
    && apt-get install -y --no-install-recommends ffmpeg \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Volta para o usuário da aplicação
USER $APP_UID

WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# =========================
# Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["AudioConverter/AudioConverter.csproj", "AudioConverter/"]
COPY ["BuildingBlocks.Messaging/BuildingBlocks.Messaging.csproj", "BuildingBlocks.Messaging/"]
COPY ["Shared.Events/Shared.Events.csproj", "Shared.Events/"]

RUN dotnet restore "./AudioConverter/AudioConverter.csproj"

COPY . .
WORKDIR "/src/AudioConverter"
RUN dotnet build "./AudioConverter.csproj" -c $BUILD_CONFIGURATION -o /app/build


# =========================
# Publish
# =========================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AudioConverter.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false


# =========================
# Final
# =========================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AudioConverter.dll"]