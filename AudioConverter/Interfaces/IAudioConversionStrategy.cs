namespace AudioConverter.Interfaces
{
    public interface IAudioConversionStrategy
    {
        Task Start(CancellationToken cancellationToken  );
    }
}