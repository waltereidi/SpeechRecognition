using Google.Cloud.Firestore;

namespace SpeechRecognition.Infra.Firestore.Documents
{
    [FirestoreData]
    public class AudioTranslationDocument
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Translation { get; set; }

        [FirestoreProperty]
        public string FileStorageId { get; set; }

        [FirestoreProperty]
        public bool IsSuccess { get; set; }

        [FirestoreProperty]
        public bool? IsApproved { get; set; }

        [FirestoreProperty]
        public int? TranslationTemplate { get; set; }

        [FirestoreProperty]
        public int? WhisperModel { get; set; }
    }
}
