using SpeechRecognition.Dominio.Entidades;
using WhisperSpeechRecognition.Templates;

namespace WhisperSpeechRecognition.Interfaces
{
    public interface ISpeechRecognitionStrategy
    {
        Task<TranslationTemplateModel> Start(Stream stream);
    }
}