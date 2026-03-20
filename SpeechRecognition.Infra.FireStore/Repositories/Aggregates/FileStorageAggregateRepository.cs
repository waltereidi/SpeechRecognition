using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using SpeechRecognition.Application.Interfaces;
using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.Infra.Firestore;
using SpeechRecognition.Infra.FireStore.Documents;
using SpeechRecognition.Infra.FireStore.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.FireStore.Repositories.Aggregates
{

    public class FireStoreFileStorageAggregateRepository
        : FirestoreRepositoryBase<FileStorageAggregate, FileStorageAggregateId>,
          IFileStorageAggregateRepository,
          IDisposable
    {
        private readonly FirestoreDb _db;

        public FireStoreFileStorageAggregateRepository(FirestoreDb db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> Exists(FileStorageAggregateId id)
        {
            var docRef = _collection.Document(id.Value.ToString());

            var snapshot = await docRef.GetSnapshotAsync();

            return snapshot.Exists;
        }

        public async Task<FileStorageAggregate?> Load(FileStorageAggregateId id)
        {
            var docRef = _collection.Document(id.ToString());

            var snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return null;

            var result = snapshot.ConvertTo<FileStorageAggregateDocument>();
            return FileStorageAggregateMapper.ToDomain(result);
        }

        public void Dispose()
        {
            // FirestoreDb não precisa ser descartado,
            // mas mantemos para compatibilidade com a interface.
        }
    }
}