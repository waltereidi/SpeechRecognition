using SpeechRecognition.CrossCutting.Framework;
using SpeechRecognition.FileStorageDomain.DomainEvents;
using SpeechRecognition.FileStorageDomain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SpeechRecognition.FileStorageDomain
{
    public class FileStorageAggregate : AggregateRoot<FileStorageAggregateId>
    {

        public FileStorageAggregate(FileStorageAggregateId id )
        {
            Apply( new Events.FileStorageAggregateCreated(id ));
        }

        public List<FileStorage> FileStorages { get; private set; }
        public List<FileStorageConversion> FileStorageConversions { get; private set; }
        public List<AudioTranslation> AudioTranslations { get; private set; }
        public List<RabbitMqLog> Logs { get; private set; }
        protected override void EnsureValidState()
        {
            throw new NotImplementedException();
        }
        public void AddFileStorageLocal(FileInfo fi , string originalFileName )
            => Apply(new Events.FileStorageAdded(Guid.NewGuid(), fi, originalFileName));
        
        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.FileStorageAggregateCreated e:
                    Id = new FileStorageAggregateId( e.aggId);
                    break;
                case Events.FileStorageAdded e:
                    var fs2 = new FileStorage(Apply);
                    ApplyToEntity(fs2, e);
                    FileStorages.Add(fs2);
                    break;
                

            }
        }
    }
}
