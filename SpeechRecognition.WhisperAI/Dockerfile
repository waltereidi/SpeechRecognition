# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

# Cria diretório padrão Linux correto
RUN mkdir -p /var/lib/SpeechRecognition/RawAudio \
    && mkdir -p /var/lib/SpeechRecognition/ConvertedAudio

# Dá permissão ao usuário "app" da imagem oficial .NET
RUN chown -R $APP_UID:$APP_UID /var/lib/SpeechRecognition \
    && chmod -R 775 /var/lib/SpeechRecognition

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SpeechRecognition.WhisperAI/SpeechRecognition.WhisperAI.csproj", "SpeechRecognition.WhisperAI/"]
COPY ["SpeechRecognition.CrossCutting.BuildingBlocks.Messaging/SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.csproj", "SpeechRecognition.CrossCutting.BuildingBlocks.Messaging/"]
COPY ["SpeechRecognition.CrossCutting.Shared.Events/SpeechRecognition.CrossCutting.Shared.Events.csproj", "SpeechRecognition.CrossCutting.Shared.Events/"]
RUN dotnet restore "./SpeechRecognition.WhisperAI/SpeechRecognition.WhisperAI.csproj"
COPY . .
WORKDIR "/src/SpeechRecognition.WhisperAI"
RUN dotnet build "./SpeechRecognition.WhisperAI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SpeechRecognition.WhisperAI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpeechRecognition.WhisperAI.dll"]