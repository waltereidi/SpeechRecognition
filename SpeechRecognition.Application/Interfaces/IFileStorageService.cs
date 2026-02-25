
namespace SpeechRecognition.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<FileInfo> SaveFile();
    }
}
