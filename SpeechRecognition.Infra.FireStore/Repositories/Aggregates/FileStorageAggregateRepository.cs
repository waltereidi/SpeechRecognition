using Google.Cloud.Firestore;
using SpeechRecognition.Application.Interfaces;
using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.Infra.Firestore;
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
            var docRef = _collection.Document(id.ToString());

            var snapshot = await docRef.GetSnapshotAsync();

            return snapshot.Exists;
        }

        public async Task<FileStorageAggregate?> Load(FileStorageAggregateId id)
        {
            var docRef = _collection.Document(id.ToString());

            var snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return null;

            return snapshot.ConvertTo<FileStorageAggregate>();
        }

        public void Dispose()
        {
            // FirestoreDb não precisa ser descartado,
            // mas mantemos para compatibilidade com a interface.
        }
    }
}