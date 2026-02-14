using SpeechRecognition.FileStorageDomain.Entidades;

namespace SpeechRecognition.AudioRecorder.Api.Interfaces
{
    public interface IFileStorageService
    {
        Task<FileInfo> SaveFile();
    }
}
