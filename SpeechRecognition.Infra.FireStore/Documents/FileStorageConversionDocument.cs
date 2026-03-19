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

    }
}