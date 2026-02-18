using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.FileStorageDomain.DomainEvents
{
    public class Events
    {
        public record FileStorageAggregateCreated(FileStorageAggregateId aggId );
        public record FileStorageAdded(Guid fsId , FileInfo fi , string originalFileName);
    }
}
