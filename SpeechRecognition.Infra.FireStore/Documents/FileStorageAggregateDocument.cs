using Google.Cloud.Firestore;
using SpeechRecognition.Infra.Firestore.Documents;
using SpeechRecognition.Infra.FireStore.Documents.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.FireStore.Documents
{
    [FirestoreData]
    public class FileStorageAggregateDocument :FireStoreBaseDocument
    {
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
