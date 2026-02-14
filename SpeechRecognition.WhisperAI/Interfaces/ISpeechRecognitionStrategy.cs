using SpeechRecognition.WhisperAI.Templates;

namespace SpeechRecognition.WhisperAI.Interfaces
{
    public interface ISpeechRecognitionStrategy
    {
        Task<TranslationTemplateModel> Start(Stream stream);
    }
}