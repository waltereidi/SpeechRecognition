using Google.Cloud.Firestore;
using SpeechRecognition.Infra.Firestore.Attributes;
using System;
using System.Text.Json;

namespace SpeechRecognition.Infra.Firestore.Documents
{
    [FirestoreCollection("audioTranslations")]
    [FirestoreData]
    public class AudioTranslationDocument
    {
        [FirestoreDocumentId]
        public string Id { get; set; } = string.Empty;

        [FirestoreProperty("fileStorageId")]
        public string FileStorageId { get; set; } = string.Empty;

        [FirestoreProperty("language")]
        public string Language { get; set; } = string.Empty;

        [FirestoreProperty("transcription")]
        public string Transcription { get; set; } = string.Empty;

        [FirestoreProperty("confidence")]
        public double? Confidence { get; set; }

        [FirestoreProperty("status")]
        public string Status { get; set; } = "Pending";

        [FirestoreProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [FirestoreProperty("completedAt")]
        public DateTime? CompletedAt { get; set; }

        // Conversões genéricas usando System.Text.Json para facilitar mapeamentos personalizados
        public static AudioTranslationDocument FromModel<TModel>(TModel model)
        {
            var json = JsonSerializer.Serialize(model, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return JsonSerializer.Deserialize<AudioTranslationDocument>(json) ?? new AudioTranslationDocument();
        }

        public TModel ToModel<TModel>()
        {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return JsonSerializer.Deserialize<TModel>(json)!;
        }
    }
}