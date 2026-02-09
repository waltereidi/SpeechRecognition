using SpeechRecognition.Dominio.Entidades;

namespace AudioRecorder.Api.Interfaces
{
    public interface IFileStorageService
    {
        Task<FileStorage> SaveFile();
    }
}
