using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.FileStorageDomain.Events
{
    public class Events
    {
        public record FileStorageCreated(Guid Id, string FileName, string FilePath, long FileSize, DateTime CreatedAt);
    }
}
