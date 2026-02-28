using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechRecognition.CrossCutting.Shared.Events.AudioRecorderApi
{
    public record class SaveAudioConversionSuccessEvent : IntegrationEvent
    {
        public string FileFullPath { get; set; }
        public string FileName { get; set; }
        public string FileStorageId { get; set; }
        public string FileStorageAggregateId { get; set; }
    }
}
