using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.Infra.FireStore.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.FireStore.DomainMapper
{
    public static class FileStorageAggregateDomainMapper
    {
        public static FileStorageAggregate ToDomain(
            FileStorageAggregateDocument doc,
            Action<object> applier)
        {
            var aggregate = new FileStorageAggregate(
                new FileStorageAggregateId(Guid.Parse(doc.Id)));

            if (doc.FileStorages != null)
            {
                aggregate.SetFileStorages(doc.FileStorages) 
                    .Select(x => FileStorageDomainMapper.ToDomain(x, applier))
                    .ToList();
            }

            if (doc.FileStorageConversions != null)
            {
                aggregate.FileStorageConversions = doc.FileStorageConversions
                    .Select(x => FileStorageConversionDomainMapper.ToDomain(x, applier))
                    .ToList();
            }

            if (doc.AudioTranslations != null)
            {
                aggregate.AudioTranslations = doc.AudioTranslations
                    .Select(x => AudioTranslationDomainMapper.ToDomain(x, applier))
                    .ToList();
            }

            if (doc.Logs != null)
            {
                aggregate.Logs = doc.Logs
                    .Select(x => RabbitMqLogDomainMapper.ToDomain(x, applier))
                    .ToList();
            }

            return aggregate;
        }
    }
}
