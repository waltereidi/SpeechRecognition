using Google.Cloud.Firestore;
using SpeechRecognition.FileStorageDomain.Entidades;

namespace SpeechRecognition.Infra.Firestore.Documents
{

    [FirestoreData]
    public class FileStorageDocument
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string? OriginalFileName { get; set; }

        [FirestoreProperty]
        public FileInfoDocument FileInfo { get; set; }
        
    }

    [FirestoreData]
    public class FileInfoDocument
    {
        [FirestoreProperty]
        public string FullName { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public long Length { get; set; }

        [FirestoreProperty]
        public string Extension { get; set; }

        [FirestoreProperty]
        public DateTime CreationTimeUtc { get; set; }
    }
}