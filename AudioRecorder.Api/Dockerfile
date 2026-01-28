# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AudioRecorder.Api/AudioRecorder.Api.csproj", "AudioRecorder.Api/"]
COPY ["SpeechRecognition.Infra/SpeechRecognition.Infra.csproj", "SpeechRecognition.Infra/"]
COPY ["SpeechRecognition.Dominio/SpeechRecognition.Dominio.csproj", "SpeechRecognition.Dominio/"]
RUN dotnet restore "./AudioRecorder.Api/AudioRecorder.Api.csproj"
COPY . .
WORKDIR "/src/AudioRecorder.Api"
RUN dotnet build "./AudioRecorder.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AudioRecorder.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AudioRecorder.Api.dll"]