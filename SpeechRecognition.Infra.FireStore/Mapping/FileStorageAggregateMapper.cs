using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.Infra.Firestore.Documents;
using SpeechRecognition.Infra.FireStore.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.FireStore.Mapping
{
    public static class FileStorageAggregateMapper
    {
        public static FileStorageAggregateDocument ToDocument(FileStorageAggregate aggregate)
        {
            var result=   new FileStorageAggregateDocument
            {
                Id = aggregate.Id.ToString(),

                FileStorages = aggregate.FileStorages?
                    .Select(FileStorageDocumentMapper.ToDocument)
                    .ToList(),

                FileStorageConversions = aggregate.FileStorageConversions?
                    .Select(FileStorageConversionDocumentMapper.ToDocument)
                    .ToList(),

                AudioTranslations = aggregate.AudioTranslations?
                    .Select(AudioTranslationDocumentMapper.ToDocument)
                    .ToList(),

                Logs = aggregate.Logs?
                    .Select(RabbitMqLogDocumentMapper.ToDocument)
                    .ToList()
            };
            return result;
        }
        public static FileStorageAggregate ToDomain(FileStorageAggregateDocument doc)
        {
            if (doc == null)
                return null;

            // cria o aggregate com o ID vindo do documento
            var aggregate = new FileStorageAggregate(
                new FileStorageAggregateId( Guid.Parse(doc.Id) )
            );

            // ⚠️ IMPORTANTE:
            // Aqui NÃO usamos Apply (event sourcing),
            // pois estamos apenas reconstruindo estado (hydration)

            aggregate.SetFileStorages(

                doc.FileStorages?
                    .Select(FileStorageDocumentMapper.ToDomain)
                    .ToList()
            );

            aggregate.SetFileStorageConversions(
                doc.FileStorageConversions?
                    .Select(FileStorageConversionDocument.ToDomain)
                    .ToList()
            );

            aggregate.SetAudioTranslations(
                doc.AudioTranslations?
                    .Select(AudioTranslationDocumentMapper.ToDomain)
                    .ToList()
            );

            aggregate.SetLogs(
                doc.Logs?
                    .Select(RabbitMqLogDocumentMapper.ToDomain)
                    .ToList()
            );

            return aggregate;
        }
    }
}
