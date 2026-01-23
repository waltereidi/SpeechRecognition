using Whisper.net;
using WhisperSpeechRecognition.Enum;
using WhisperSpeechRecognition.Interfaces;
using WhisperSpeechRecognition.Templates;

namespace WhisperSpeechRecognition.Service
{
    internal abstract class WhisperModel : ISpeechRecognitionStrategy
    {
        private readonly WhisperFactory? _factory;
        private readonly WhisperProcessor? _processor;
        private readonly TranslationTemplateModel _template;
        private readonly WhisperModels _model;
        public WhisperModel(TranslationTemplateModel template , WhisperModels model )
        {
            _template = template;
            _model = model;

            string modelPath = GetModelPath();
            
            if (!File.Exists(modelPath))
                throw new FileNotFoundException("Modelo não encontrado.", modelPath);

            _factory = WhisperFactory.FromPath(modelPath);

            _processor = _factory.CreateBuilder()
                .WithLanguage("pt")
                .Build();
        }

        private string GetModelPath()
        {
            switch (_model)
            {
                case WhisperModels.Medium:
                    return Path.Combine(AppContext.BaseDirectory, "Models", "ggml-medium.bin");break;
                case WhisperModels.Small: 
                    return Path.Combine(AppContext.BaseDirectory, "Models", "ggml-small.bin");break;
                case WhisperModels.Tiny:
                    return Path.Combine(AppContext.BaseDirectory, "Models", "ggml-tiny.bin");break;
                default: throw new NotImplementedException($"Modelo não implementado. {nameof(_model)}");
            }            
        }

        public async Task<TranslationTemplateModel> Start(Stream stream )
        {
            await foreach (var segment in _processor.ProcessAsync(stream) )
                _template.AddSegment(segment);

            return _template;
        }

  
    }

}
