namespace SpeechRecognition.AudioConverter.Api.Interfaces
{
    public interface IAudioConversionResponseAdapter
    {
        public string GetResultFileName();
        public FileInfo GetResultFileInfo();
    }
}
