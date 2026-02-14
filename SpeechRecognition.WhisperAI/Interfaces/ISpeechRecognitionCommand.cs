using SpeechRecognition.WhisperAI.DTO;

namespace SpeechRecognition.WhisperAI.Interfaces
{
    internal interface ISpeechRecognitionAbstractFactory
    {
        public Task<ITranslateAudioFacade> Create(SpeechRecognitionFactoryDTO dto);
    }
}
