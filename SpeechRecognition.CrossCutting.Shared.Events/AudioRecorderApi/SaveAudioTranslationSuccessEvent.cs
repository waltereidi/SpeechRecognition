using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechRecognition.CrossCutting.Shared.Events.AudioRecorderApi
{
    public record class SaveAudioTranslationSuccessEvent : IntegrationEvent
    {
        public string FileStorageId { get; set; }
        public string Translation { get; set; }
        public int? ModelId { get; set; }
        public int? TemplateId { get; set; }
    }
}
