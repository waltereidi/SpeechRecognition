using Google.Cloud.Firestore;
using SpeechRecognition.Infra.Firestore.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.FireStore.Documents
{
    [FirestoreData]
    public class FileStorageAggregateDocument
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public List<FileStorageDocument> FileStorages { get; set; } = new();

        [FirestoreProperty]
        public List<RabbitMqLogDocument> Logs { get; set; } = new();

        [FirestoreProperty]
        public List<AudioTranslationDocument> AudioTranslations { get; set; } = new();

        [FirestoreProperty]
        public List<FileStorageConversionDocument> FileStorageConversions { get; set; } = new();
    }
}
