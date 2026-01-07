namespace WhisperSpeechRecognition.Interfaces
{
    public interface ISpeechRecognition
    {
        Task<string> GetTranslation(Stream stream);
    }
}