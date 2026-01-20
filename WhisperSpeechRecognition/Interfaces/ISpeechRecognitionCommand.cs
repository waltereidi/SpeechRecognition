using WhisperSpeechRecognition.DTO;

namespace WhisperSpeechRecognition.Interfaces
{
    internal interface ISpeechRecognitionAbstractFactory
    {
        public Task<ITranslateAudioFacade> Create(SpeechRecognitionFactoryDTO dto);
    }
}
