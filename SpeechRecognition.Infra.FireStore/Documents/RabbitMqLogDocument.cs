using Google.Cloud.Firestore;
using SpeechRecognition.Infra.Firestore.Attributes;
using System;
using System.Text.Json;

namespace SpeechRecognition.Infra.Firestore.Documents
{
    [FirestoreCollection("rabbitMqLogs")]
    [FirestoreData]
    public class RabbitMqLogDocument
    {
        [FirestoreDocumentId]
        public string Id { get; set; } = string.Empty;

        [FirestoreProperty("exchange")]
        public string? Exchange { get; set; }

        [FirestoreProperty("routingKey")]
        public string? RoutingKey { get; set; }

        [FirestoreProperty("queue")]
        public string? Queue { get; set; }

        [FirestoreProperty("messageType")]
        public string? MessageType { get; set; }

        [FirestoreProperty("payload")]
        public string? Payload { get; set; }

        [FirestoreProperty("direction")]
        public string Direction { get; set; } = "In"; // In | Out

        [FirestoreProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [FirestoreProperty("error")]
        public string? Error { get; set; }

        public static RabbitMqLogDocument FromModel<TModel>(TModel model)
        {
            var json = JsonSerializer.Serialize(model, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return JsonSerializer.Deserialize<RabbitMqLogDocument>(json) ?? new RabbitMqLogDocument();
        }

        public TModel ToModel<TModel>()
        {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return JsonSerializer.Deserialize<TModel>(json)!;
        }
    }
}