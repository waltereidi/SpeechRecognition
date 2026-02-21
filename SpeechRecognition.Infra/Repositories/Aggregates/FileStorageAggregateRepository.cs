using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.FileStorageDomain.Interfaces;
using SpeechRecognition.Infra.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.Repositories.Aggregates
{
    public class FileStorageAggregateRepository : IFileStorageAggregateRepository , IDisposable
    {
        private readonly AppDbContext _dbContext;

        public FileStorageAggregateRepository(AppDbContext dbContext)
            => _dbContext = dbContext;

        public async Task Add(FileStorageAggregate entity)
            => await _dbContext.FileStorageAggregate.AddAsync(entity);

        public async Task<bool> Exists(FileStorageAggregateId id)
            => _dbContext.FileStorageAggregate.Any(x => x.Id == id);

        public async Task<FileStorageAggregate> Load(FileStorageAggregateId id)
            => _dbContext.FileStorageAggregate.First(x=>x.Id == id);

        public void Dispose() => _dbContext.Dispose();
    }
}
