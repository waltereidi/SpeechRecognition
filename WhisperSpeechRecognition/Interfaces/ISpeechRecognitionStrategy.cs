namespace WhisperSpeechRecognition.Interfaces
{
    public interface ISpeechRecognitionStrategy
    {
        Task<string> Start(Stream stream);
    }
}