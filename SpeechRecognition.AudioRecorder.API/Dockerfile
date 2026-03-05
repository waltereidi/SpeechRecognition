# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

USER root

# Instala bibliotecas necessárias para Whisper.NET
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        libc6 \
        libgomp1 \
        ffmpeg \
        ca-certificates \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development

USER $APP_UID


# ==============================
# Build
# ==============================

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

USER root

# Cria diretórios usados pela aplicação
RUN mkdir -p /var/lib/SpeechRecognition/RawAudio \
    && mkdir -p /var/lib/SpeechRecognition/ConvertedAudio

# Permissão ao usuário app
RUN chown -R $APP_UID:$APP_UID /var/lib/SpeechRecognition \
    && chmod -R 775 /var/lib/SpeechRecognition

USER $APP_UID

WORKDIR /src
COPY ["AudioRecorder.Api/AudioRecorder.Api.csproj", "AudioRecorder.Api/"]
COPY ["BuildingBlocks.Messaging/BuildingBlocks.Messaging.csproj", "BuildingBlocks.Messaging/"]
COPY ["Shared.Events/Shared.Events.csproj", "Shared.Events/"]
COPY ["SpeechRecognition.Infra/SpeechRecognition.Infra.csproj", "SpeechRecognition.Infra/"]
COPY ["SpeechRecognition.Dominio/SpeechRecognition.Dominio.csproj", "SpeechRecognition.Dominio/"]

RUN dotnet restore "./AudioRecorder.Api/AudioRecorder.Api.csproj"

COPY . .

WORKDIR "/src/AudioRecorder.Api"

RUN dotnet build "./AudioRecorder.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build


# ==============================
# Publish
# ==============================

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "./AudioRecorder.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false


# ==============================
# Runtime
# ==============================

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "AudioRecorder.Api.dll"]