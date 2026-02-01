using BuildingBlocks.Messaging.Events;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events.AudioRecorderApi
{
    public record class SaveAudioConversionSuccessEvent : IntegrationEvent
    {
        public string FileName { get; set; }
        public string Id { get; set; }
        public string FileFullPath { get; set; }
    }
}
