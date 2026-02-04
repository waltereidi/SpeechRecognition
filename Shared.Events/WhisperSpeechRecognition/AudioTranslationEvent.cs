using BuildingBlocks.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events.WhisperSpeechRecognition
{
    public record class AudioTranslationEvent : IntegrationEvent
    {
        public string FilePath { get; init; }
        public string FileStorageConversionId { get; init; }
        public int? TemplateId { get; init; } = null;
        public int? ModelId { get; init; } = null;
    }
}
