using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.FileStorageDomain.Interfaces
{
    public interface IFileStorageAggregateRepository
    {
        Task<FileStorageAggregate> Load(FileStorageAggregateId id);

        Task Add(FileStorageAggregate entity);

        Task<bool> Exists(FileStorageAggregateId id);
    }
}
