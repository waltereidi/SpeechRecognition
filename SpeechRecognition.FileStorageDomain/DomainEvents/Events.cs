using SpeechRecognition.FileStorageDomain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.FileStorageDomain.DomainEvents
{
    public class Events
    {
        public record FileStorageConversionAdded(FileInfo fi , FileStorageId fileId );
        public record CreateFileStorageConversion(FileStorageId fsId, FileInfo fi);
        public record FileStorageAggregateCreated(FileStorageAggregateId aggId );
        public record FileStorageAdded(Guid fsId , FileInfo fi , string originalFileName);
        public record TranslationAdded(FileStorageId fileStorageid , string translation , int? templateId , int? modelId , bool isSuccess );
        public record ErrorLog(string source,  string errorMessage , int severity , string aggegateId);
    }
}
