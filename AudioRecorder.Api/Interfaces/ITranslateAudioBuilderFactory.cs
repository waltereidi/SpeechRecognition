namespace AudioRecorder.Api.Interfaces
{
    public interface ITranslateAudioBuilderFactory
    {
        ITranslateAudioBuilder CreateFromStream(string originalFileName, Stream stream);

    }
}
