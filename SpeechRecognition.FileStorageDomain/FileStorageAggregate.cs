using SpeechRecognition.CrossCutting.Framework;
using SpeechRecognition.FileStorageDomain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.FileStorageDomain
{
    public class FileStorageAggregate : AggregateRoot<FileStorageAggregateId>
    {
        public List<FileStorage> FileStorages { get; private set; }
        public FileStorageConversion FileStorageConversion { get; private set; }
        public List<AudioTranslation> AudioTranslation { get; private set; }
        public List<RabbitMqLog> Logs { get; private set; }
        protected override void EnsureValidState()
        {
            throw new NotImplementedException();
        }

        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}
