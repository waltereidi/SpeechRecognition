using Google.Cloud.Firestore;
using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.Infra.FireStore.Documents.Base;

namespace SpeechRecognition.Infra.Firestore.Documents
{
    [FirestoreData]
    public class AudioTranslationDocument : FireStoreBaseDocument
    {

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

        public AudioTranslation ToDomain()
        {
            var e = new AudioTranslation();
            e.SetId(Id);
            e.Translation = Translation;
            e.FileStorageId = new(Guid.Parse(FileStorageId));
            e.IsSuccess = IsSuccess;
            e.IsApproved = IsApproved;
            e.TranslationTemplate = TranslationTemplate;
            e.WhisperModel = WhisperModel;

            return e;
        }
    }
}
