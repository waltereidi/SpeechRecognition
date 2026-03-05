# =========================
# Base runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
# bibliotecas necessárias para whisper.cpp
RUN apt-get update && apt-get install -y \
    libc6 \
    libgomp1 \
    libstdc++6 \
    libgcc-s1 \
    && rm -rf /var/lib/apt/lists/*
# evita crash do ggml
ENV OMP_NUM_THREADS=1
ENV OMP_WAIT_POLICY=PASSIVE
# força uso do runtime CPU padrão (evita crash do openvino no dlopen)
ENV WHISPER_RUNTIME=cpu

# =========================
# Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# copia csproj
COPY ["SpeechRecognition.WhisperAI/SpeechRecognition.WhisperAI.csproj", "SpeechRecognition.WhisperAI/"]
COPY ["SpeechRecognition.CrossCutting.BuildingBlocks.Messaging/SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.csproj", "SpeechRecognition.CrossCutting.BuildingBlocks.Messaging/"]
COPY ["SpeechRecognition.CrossCutting.Shared.Events/SpeechRecognition.CrossCutting.Shared.Events.csproj", "SpeechRecognition.CrossCutting.Shared.Events/"]
RUN dotnet restore "./SpeechRecognition.WhisperAI/SpeechRecognition.WhisperAI.csproj"
# copia restante
COPY . .
WORKDIR "/src/SpeechRecognition.WhisperAI"
RUN dotnet build "SpeechRecognition.WhisperAI.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build

# =========================
# Publish
# =========================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SpeechRecognition.WhisperAI.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    -r linux-x64 \
    --self-contained false \
    /p:UseAppHost=false

# =========================
# Final image
# =========================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# remove o runtime openvino para evitar que o dlopen tente carregá-lo
RUN rm -rf /app/runtimes/openvino
ENTRYPOINT ["dotnet", "SpeechRecognition.WhisperAI.dll"]