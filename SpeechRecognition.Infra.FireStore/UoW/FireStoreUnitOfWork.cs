using Google.Cloud.Firestore;
using SpeechRecognition.CrossCutting.Framework.Interfaces;
using SpeechRecognition.Infra.Context;

namespace SpeechRecognition.Infra.UoW
{
    public class FireStoreUnitOfWork : IUnitOfWork
    {
        private readonly FirestoreDb _db;
        private readonly WriteBatch _batch;

        public FireStoreUnitOfWork(FirestoreDb db)
        {
            _db = db;
            _batch = db.StartBatch();
        }


        public Task CommitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
