# SpeechRecognition

SpeechRecognition é um conjunto de serviços para **processamento e transcrição de áudio**, permitindo converter arquivos de áudio em texto usando modelos de reconhecimento de fala.

O projeto foi desenvolvido utilizando **.NET**, **Docker** e ferramentas de processamento de áudio como **FFmpeg**, permitindo integração com pipelines de processamento distribuído e serviços de mensageria.

---

# 🚀 Features

- 🎤 Upload e armazenamento de arquivos de áudio
- 🔊 Conversão e normalização de áudio (ex: 16kHz mono)
- 🧠 Transcrição de áudio utilizando modelos de reconhecimento de fala
- 📦 Arquitetura baseada em microsserviços
- 📨 Processamento assíncrono via mensageria
- 🐳 Suporte a execução em containers Docker

---

# 🏗 Arquitetura

O sistema é composto por múltiplos serviços especializados:

| Serviço | Descrição |
|-------|--------|
| Audio Recorder API | Responsável por receber e armazenar arquivos de áudio |
| Audio Converter API | Converte arquivos de áudio para o formato adequado |
| Speech Recognition | Processa os arquivos e gera a transcrição |
| File Storage | Armazenamento persistente dos arquivos |

Fluxo simplificado:
Client
│
▼
AudioRecorder API
│
▼
Message Queue
│
▼
AudioConverter
│
▼
SpeechRecognition
│
▼
Transcription Result

---

# 📦 Tecnologias Utilizadas

- .NET
- ASP.NET Web API
- Docker
- FFmpeg
- Whisper (Speech-to-Text)
- MassTransit
- RabbitMQ (ou outro broker)
- Entity Framework

---

# 📂 Estrutura do Projeto
SpeechRecognition
│
├── SpeechRecognition.AudioRecorder.Api
├── SpeechRecognition.AudioConverter.Api
├── SpeechRecognition.Application
├── SpeechRecognition.CrossCutting
├── SpeechRecognition.Infra
├── SpeechRecognition.FileStorageDomain
└── docker-compose.yml


---

# ⚙️ Pré-requisitos

Antes de executar o projeto, você precisa ter instalado:

- .NET SDK
- Docker
- Docker Compose
- FFmpeg

---

# 🐳 Executando com Docker

Clone o repositório:

```bash
git clone https://github.com/waltereidi/SpeechRecognition.git
cd SpeechRecognition
