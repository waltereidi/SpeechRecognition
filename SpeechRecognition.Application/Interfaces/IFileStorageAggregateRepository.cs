using SpeechRecognition.FileStorageDomain;

namespace SpeechRecognition.Application.Interfaces
{
    public interface IFileStorageAggregateRepository : IRepositoryBase<FileStorageAggregate , FileStorageAggregateId>
    {
        Task<FileStorageAggregate> Load(FileStorageAggregateId id);
        Task<bool> Exists(FileStorageAggregateId id);
    }
}
