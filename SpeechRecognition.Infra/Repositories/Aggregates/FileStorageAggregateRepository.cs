using SpeechRecognition.Application.Interfaces;
using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.Infra.Context;
using SpeechRecognition.Infra.Repositories.Base;

namespace SpeechRecognition.Infra.Repositories.Aggregates
{
    public class FileStorageAggregateRepository 
        : RepositoryBase<AppDbContext, FileStorageAggregate, FileStorageAggregateId>
        , IFileStorageAggregateRepository
        , IDisposable
    {
        private readonly AppDbContext _dbContext;

        public FileStorageAggregateRepository(AppDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<bool> Exists(FileStorageAggregateId id)
            => _dbContext.FileStorageAggregate.Any(x => x.Id == id);

        public async Task<FileStorageAggregate> Load(FileStorageAggregateId id)
            => _dbContext.FileStorageAggregate.First(x=>x.Id == id);

        public void Dispose() => _dbContext.Dispose();
    }
}
