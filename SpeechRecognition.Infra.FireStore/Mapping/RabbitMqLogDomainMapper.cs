using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.FileStorageDomain.Enum;
using SpeechRecognition.Infra.Firestore.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.FireStore.Mapping
{
    public static class RabbitMqLogDocumentMapper
    {
        public static RabbitMqLogDocument ToDocument(RabbitMqLog entity)
        {
            return new RabbitMqLogDocument
            {
                Id = entity.Id.ToString(),
                Description = entity.Description,
                Severity = (int)entity.Severity
            };
        }
        public static RabbitMqLog ToDomain(RabbitMqLogDocument doc)
        {
            if (doc == null)
                return null;

            var entity = new RabbitMqLog();

            // Id
            if (!string.IsNullOrEmpty(doc.Id))
                entity.SetId(doc.Id);

            // Propriedades
            entity.Description = doc.Description;
            entity.Severity = (LogSeverity)doc.Severity;

            return entity;
        }
    }
}
