using Google.Cloud.Firestore;
using SpeechRecognition.Infra.Firestore.Attributes;
using System;
using System.Text.Json;

namespace SpeechRecognition.Infra.Firestore.Documents
{
    [FirestoreCollection("fileStorageConversions")]
    [FirestoreData]
    public class FileStorageConversionDocument
    {
        [FirestoreDocumentId]
        public string Id { get; set; } = string.Empty;

        [FirestoreProperty("sourceFileStorageId")]
        public string SourceFileStorageId { get; set; } = string.Empty;

        [FirestoreProperty("targetFileStorageId")]
        public string TargetFileStorageId { get; set; } = string.Empty;

        [FirestoreProperty("format")]
        public string Format { get; set; } = string.Empty;

        [FirestoreProperty("status")]
        public string Status { get; set; } = "Pending";

        [FirestoreProperty("errorMessage")]
        public string? ErrorMessage { get; set; }

        [FirestoreProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [FirestoreProperty("completedAt")]
        public DateTime? CompletedAt { get; set; }

        public static FileStorageConversionDocument FromModel<TModel>(TModel model)
        {
            var json = JsonSerializer.Serialize(model, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return JsonSerializer.Deserialize<FileStorageConversionDocument>(json) ?? new FileStorageConversionDocument();
        }

        public TModel ToModel<TModel>()
        {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return JsonSerializer.Deserialize<TModel>(json)!;
        }
    }
}