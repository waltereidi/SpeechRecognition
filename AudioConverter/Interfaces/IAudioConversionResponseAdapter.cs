namespace AudioConverter.Interfaces
{
    public interface IAudioConversionResponseAdapter
    {
        public string GetResultFileName();
        public FileInfo GetResultFileInfo();
    }
}
