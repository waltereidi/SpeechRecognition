using Google.Cloud.Firestore;
using SpeechRecognition.FileStorageDomain.Entidades;
using static SpeechRecognition.FileStorageDomain.Entidades.FileStorage;

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
        
        public FileStorage ToDomain()
        {
            var e = new FileStorage();
            e.SetId(Id);


            e.OriginalFileName = OriginalFileName;
            e.FileInfo = new( FileInfo.FullName);
            
            return e;
        }
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