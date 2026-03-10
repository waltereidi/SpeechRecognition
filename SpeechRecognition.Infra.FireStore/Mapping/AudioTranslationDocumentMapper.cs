using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.Infra.Firestore.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.FireStore.Mapping
{
    public static class AudioTranslationDocumentMapper
    {
        public static AudioTranslationDocument ToDocument(AudioTranslation entity)
        {
            return new AudioTranslationDocument
            {
                Id = entity.Id.ToString(),
                Translation = entity.Translation,
                FileStorageId = entity.FileStorageId.ToString(),
                IsSuccess = entity.IsSuccess,
                IsApproved = entity.IsApproved,
                TranslationTemplate = entity.TranslationTemplate,
                WhisperModel = entity.WhisperModel
            };
        }
    }
}
