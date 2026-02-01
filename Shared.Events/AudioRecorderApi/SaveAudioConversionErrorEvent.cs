using BuildingBlocks.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events.AudioRecorderApi
{
    public record class SaveAudioConversionErrorEvent : IntegrationEvent
    {
        public string FileStorageId { get; init; }
        public string ErrorMessage { get; init; }
    }
}
