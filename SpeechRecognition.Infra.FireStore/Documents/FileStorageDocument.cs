using Google.Cloud.Firestore;
using SpeechRecognition.Domain.FileStorage.Aggregates;
using SpeechRecognition.Infra.Firestore.Attributes;
using System;
using System.Text.Json;

namespace SpeechRecognition.Infra.Firestore.Documents
{
    [FirestoreCollection("fileStorages")]
    [FirestoreData]
    public class FileStorageDocument
    {
        [FirestoreDocumentId]
        public string Id { get; set; } = string.Empty;

        [FirestoreProperty("fileName")]
        public string FileName { get; set; } = string.Empty;

        [FirestoreProperty("contentType")]
        public string ContentType { get; set; } = string.Empty;

        [FirestoreProperty("size")]
        public long Size { get; set; }

        [FirestoreProperty("path")]
        public string Path { get; set; } = string.Empty;

        [FirestoreProperty("provider")]
        public string Provider { get; set; } = StorageProvider.Local.ToString();

        [FirestoreProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [FirestoreProperty("deletedAt")]
        public DateTime? DeletedAt { get; set; }

        public static FileStorageDocument FromAggregate(FileStorageAggregate agg)
        {
            return new FileStorageDocument
            {
                Id = agg.Id.ToString(),
                FileName = agg.FileName,
                ContentType = agg.ContentType,
                Size = agg.Size,
                Path = agg.Path,
                Provider = agg.Provider.ToString(),
                CreatedAt = agg.CreatedAt,
                DeletedAt = agg.DeletedAt
            };
        }

        public FileStorageAggregate ToAggregate()
        {
            // tenta converter o Id para Guid quando possível, senão gera novo Guid
            Guid id = Guid.TryParse(Id, out var g) ? g : Guid.NewGuid();
            var provider = Enum.TryParse<StorageProvider>(Provider, out var p) ? p : StorageProvider.Local;

            var agg = new FileStorageAggregate(id, FileName, ContentType, Size, Path, provider);
            if (DeletedAt.HasValue)
                agg.MarkDeleted();
            return agg;
        }

        public static FileStorageDocument FromModel<TModel>(TModel model)
        {
            var json = JsonSerializer.Serialize(model, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return JsonSerializer.Deserialize<FileStorageDocument>(json) ?? new FileStorageDocument();
        }

        public TModel ToModel<TModel>()
        {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return JsonSerializer.Deserialize<TModel>(json)!;
        }
    }
}