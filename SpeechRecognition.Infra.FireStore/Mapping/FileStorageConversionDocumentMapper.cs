using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.Infra.Firestore.Documents;

namespace SpeechRecognition.Infra.FireStore.Mapping
{
    public static class FileStorageConversionDocumentMapper
    {
        public static FileStorageConversionDocument ToDocument(FileStorageConversion entity)
        {
            return new FileStorageConversionDocument
            {
                Id = entity.Id.ToString(),
                FileStorageId = entity.FileStorageId.ToString()
            };
        }
    }
}
