namespace SpeechRecognition.AudioConverter.Api.Interfaces
{
    public interface IAudioConversionFactory
    {
        public IAudioConversionResponseAdapter GetAdapter();
        public IAudioConversionStrategy GetStrategy();
    }
}
