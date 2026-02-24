using SpeechRecognition.FileStorageDomain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.FileStorageDomain.DomainEvents
{
    public class Events
    {
        public record FileStorageConversionAdded(FileStorageId fsId, FileInfo fi  );
        public record FileStorageAggregateCreated(FileStorageAggregateId aggId );
        public record FileStorageAdded(Guid fsId , FileInfo fi , string originalFileName);
    }
}
