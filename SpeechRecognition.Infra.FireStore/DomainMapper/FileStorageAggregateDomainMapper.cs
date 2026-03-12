using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.FileStorageDomain.Entidades;
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
                var entities = doc.FileStorages
                    .Select(s=> s.ToDomain() )
                    .ToList();

                aggregate.SetFileStorages(entities);
            }

            if (doc.FileStorageConversions != null)
            {
                var entities = doc.FileStorageConversions
                    .Select(s => s.ToDomain())
                    .ToList();

                //aggregate.FileStorageConversions = doc.FileStorageConversions.
            }

            if (doc.AudioTranslations != null)
            {
                
                var tranlations = doc.AudioTranslations
                    .Select( s=> s.ToDomain())
                    .ToList();

                aggregate.SetAudioTranslations(tranlations);
            }

            //if (doc.Logs != null)
            //{
            //    aggregate.Logs = doc.Logs
            //        .Select(x => RabbitMqLogDomainMapper.ToDomain(x, applier))
            //        .ToList();
            //}


            return aggregate;
        }
    }
}
