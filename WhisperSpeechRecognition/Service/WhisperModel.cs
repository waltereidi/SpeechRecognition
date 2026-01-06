using System;
using System.Collections.Generic;
using System.Text;
using Whisper.net;
using WhisperSpeechRecognition.Interfaces;

namespace WhisperSpeechRecognition.Service
{
    public class WhisperModel : ISpeechModelFactory
    {
        private readonly WhisperFactory? _factory;
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
        }


    }

}
