using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.FileStorageDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.Repositories.Aggregates
{
    public class FileStorageAggregateRepository : IFileStorageAggregateRepository , IDisposable
    {

        public Task Add(FileStorageAggregate entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(FileStorageAggregateId id)
        {
            throw new NotImplementedException();
        }

        public Task<FileStorageAggregate> Load(FileStorageAggregateId id)
        {
            throw new NotImplementedException();
        }
    }
}
