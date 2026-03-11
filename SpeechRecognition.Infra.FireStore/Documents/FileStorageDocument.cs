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
        
        public FileStorage ToFileStorage()
        {
            var e = new FileStorage();
            e.Id = new FileStorageId(Guid.Parse(Id));

            e.OriginalFileName = OriginalFile
            return new FileStorage
            {
                Id = new FileStorageId(Guid.Parse(Id)),
                OriginalFileName = OriginalFileName,
                FileInfo = new FileInfo
                {
                    FullName = FileInfo.FullName,
                    Name = FileInfo.Name,
                    Length = FileInfo.Length,
                    Extension = FileInfo.Extension,
                    CreationTimeUtc = FileInfo.CreationTimeUtc
                }
            };
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