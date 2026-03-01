using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechRecognition.CrossCutting.Shared.Events.Generic
{
    public record class ErrorLogEvent : IntegrationEvent
    {
        public string Source { get; init; }
        public string ErrorMessage { get; init; }
        /// <summary>
        /// Severity level of the error (e.g., 1-5)
        /// 1 - Critical
        /// 2 - High
        /// 3 - Medium
        /// 4 - Low
        /// 5 - Informational
        /// </summary>
        public int Severity { get; init; }
        public string AggregateId { get; init; }
    }
}
