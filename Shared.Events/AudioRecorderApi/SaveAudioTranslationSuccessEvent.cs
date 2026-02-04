using BuildingBlocks.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events.AudioRecorderApi
{
    public record class SaveAudioTranslationSuccessEvent : IntegrationEvent
    {
        public string FileStorageConversionId { get; init; }
        public string Translation { get; init; }
        public int ModelId { get; init; }
    }
}
