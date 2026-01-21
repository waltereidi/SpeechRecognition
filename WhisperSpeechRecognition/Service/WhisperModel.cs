using Whisper.net;
using WhisperSpeechRecognition.Interfaces;
using WhisperSpeechRecognition.Templates;

namespace WhisperSpeechRecognition.Service
{
    public abstract class WhisperModel : ISpeechRecognitionStrategy
    {
        private readonly WhisperFactory? _factory;
        private readonly WhisperProcessor? _processor;
        private readonly TranslationTemplateModel _template;
        public WhisperModel(TranslationTemplateModel template )
        {
            string modelPath = Path.Combine( AppContext.BaseDirectory, "Models", "ggml-medium.bin");
            
            if (!File.Exists(modelPath))
                throw new FileNotFoundException("Modelo não encontrado.", modelPath);

            _factory = WhisperFactory.FromPath(modelPath);

            _processor = _factory.CreateBuilder()
                .WithLanguage("pt")
                .Build();

            _template = template;
        }

        public async Task<TranslationTemplateModel> Start(Stream stream )
        {
            await foreach (var segment in _processor.ProcessAsync(stream) )
                _template.AddSegment(segment);

            return _template;
        }

  
    }

}
