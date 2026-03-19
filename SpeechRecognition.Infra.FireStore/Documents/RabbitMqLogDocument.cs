using Google.Cloud.Firestore;
using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.FileStorageDomain.Enum;
using SpeechRecognition.Infra.Firestore.Attributes;
using SpeechRecognition.Infra.FireStore.Documents.Base;
using System;
using System.Text.Json;

namespace SpeechRecognition.Infra.Firestore.Documents
{

    [FirestoreData]
    public class RabbitMqLogDocument : FireStoreBaseDocument
    {
        [FirestoreProperty]
        public string Description { get; set; }

        [FirestoreProperty]
        public int Severity { get; set; }
        public RabbitMqLog ToDomain()
        {
            var e = new RabbitMqLog();
            e.Severity = (LogSeverity)Severity;
            e.Description = Description;
            e.SetId(Id);

            return e;
        }
    }
}