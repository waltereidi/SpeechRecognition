using SpeechRecognition.Application.Services;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Shared.Events.Generic;
using System.Runtime.Intrinsics;
using static SpeechRecognition.Application.Contracts.FileStorageAggregateContract;

namespace SpeechRecognition.AudioRecorder.Api.Interfaces
{
    public class RabbitMqLogHandler : IIntegrationEventHandler<ErrorLogEvent>
    { 
        private readonly ILogger<ErrorLogEvent> _logger;
        private readonly FileStorageAggregateApplicationService _service;
        public RabbitMqLogHandler(IEventBus eventBus, 
            ILogger<ErrorLogEvent> logger ,
            FileStorageAggregateApplicationService service
            )
        {
            _logger = logger;
            _service = service;
        }
        public async Task HandleAsync(ErrorLogEvent @event, CancellationToken cancellationToken = default)
        {
            V1.ErrorLog logEvent = new V1.ErrorLog(@event.Source,@event.ErrorMessage,@event.Severity, @event.AggregateId);
            await _service.Handle(@event);

        }
    }
}
