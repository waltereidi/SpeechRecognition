using Google.Cloud.Firestore;
using SpeechRecognition.Infra.Firestore.Attributes;
using System;
using System.Text.Json;

namespace SpeechRecognition.Infra.Firestore.Documents
{

    [FirestoreData]
    public class RabbitMqLogDocument
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Description { get; set; }

        [FirestoreProperty]
        public int Severity { get; set; }
    }
}