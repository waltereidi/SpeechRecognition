using System;
using System.Collections.Generic;
using System.Text;
using Whisper.net;
using WhisperSpeechRecognition.Interfaces;

namespace WhisperSpeechRecognition.Service
{
    public class WhisperModel : ISpeechRecognition
    {
        private readonly WhisperFactory? _factory;
        private readonly WhisperProcessor? _processor;
        public WhisperModel()
        {
            string modelPath = Path.Combine(
                AppContext.BaseDirectory,
                "Models",
                "ggml-medium.bin"
            );
            if (!File.Exists(modelPath))
            {
                throw new FileNotFoundException("Modelo não encontrado.", modelPath);
            }
            _factory = WhisperFactory.FromPath(modelPath);
            _processor = _factory.CreateBuilder()
                .WithLanguage("pt")          // pt-br
                .Build();
        }

        public async Task<string> GetTranslation(Stream stream)
        {
            string result = string.Empty;
            await foreach (var segment in _processor.ProcessAsync(stream))
            {
                result += $"[{segment.Start:hh\\:mm\\:ss} -> {segment.End:hh\\:mm\\:ss}] {segment.Text}";
            }
            return result;
        }
    }

}
