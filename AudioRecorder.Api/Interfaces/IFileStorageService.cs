namespace AudioRecorder.Api.Interfaces
{
    public interface IFileStorageService
    {
        FileInfo SaveFile(Stream stream);
        string GetFullFileName();
    }
}
