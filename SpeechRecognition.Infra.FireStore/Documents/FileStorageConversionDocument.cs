using Google.Cloud.Firestore;
using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.Infra.Firestore.Attributes;
using SpeechRecognition.Infra.FireStore.Documents.Base;
using System;
using System.Text.Json;

namespace SpeechRecognition.Infra.Firestore.Documents
{
    [FirestoreData]
    public class FileStorageConversionDocument : FireStoreBaseDocument
    {

        [FirestoreProperty]
        public string FileStorageId { get; set; }
        public FileStorageConversion ToDomain()
        {
            var e = new FileStorageConversion();
            e.FileStorageId = new FileStorageId(Guid.Parse(FileStorageId));

            return e;
        }
        public static FileStorageConversion ToDomain(FileStorageConversionDocument doc)
        {
            if (doc == null)
                return null;

            var entity = new FileStorageConversion();

            // Id da conversão
            if (!string.IsNullOrEmpty(doc.Id))
                entity.SetId(doc.Id);

            // Relacionamento com FileStorage
            if (!string.IsNullOrEmpty(doc.FileStorageId))
                entity.FileStorageId = new FileStorageId(Guid.Parse(doc.FileStorageId));

            return entity;
        }

    }
}