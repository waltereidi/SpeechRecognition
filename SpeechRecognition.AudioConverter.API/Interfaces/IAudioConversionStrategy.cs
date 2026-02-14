namespace SpeechRecognition.AudioConverter.Api.Interfaces
{
    public interface IAudioConversionStrategy
    {
        Task Start(CancellationToken cancellationToken  );
    }
}