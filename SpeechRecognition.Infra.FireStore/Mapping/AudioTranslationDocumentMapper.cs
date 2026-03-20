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
        public static AudioTranslation ToDomain(AudioTranslationDocument doc)
        {
            if (doc == null)
                return null;

            var entity = new AudioTranslation();

            // Id
            if (!string.IsNullOrEmpty(doc.Id))
                entity.SetId(doc.Id);

            // Relacionamento
            if (!string.IsNullOrEmpty(doc.FileStorageId))
                entity.FileStorageId = new FileStorageId(Guid.Parse(doc.FileStorageId));

            // Propriedades simples
            entity.Translation = doc.Translation;
            entity.IsSuccess = doc.IsSuccess;
            entity.IsApproved = doc.IsApproved;
            entity.TranslationTemplate = doc.TranslationTemplate;
            entity.WhisperModel = doc.WhisperModel;

            return entity;
        }
    }
}
