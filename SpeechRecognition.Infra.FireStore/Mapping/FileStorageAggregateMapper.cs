using SpeechRecognition.FileStorageDomain;
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
            return new FileStorageAggregateDocument
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
        }
    }
}
