namespace AudioConverter.Interfaces
{
    public interface IAudioConversionFactory
    {
        public IAudioConversionResponseAdapter GetAdapter();
        public IAudioConversionStrategy GetStrategy();
    }
}
