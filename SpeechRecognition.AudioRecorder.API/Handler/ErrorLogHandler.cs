using SpeechRecognition.AudioRecorder.Api.Services;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Shared.Events.Generic;

namespace SpeechRecognition.AudioRecorder.Api.Interfaces
{
    public class RabbitMqLogHandler : IIntegrationEventHandler<ErrorLogEvent>
    { 
        private readonly ILogger<ErrorLogEvent> _logger;
        private readonly RabbitMqLogService _service;
        public RabbitMqLogHandler(IEventBus eventBus, ILogger<ErrorLogEvent> logger , RabbitMqLogService service )
        {
            _logger = logger;
            _service = service;
        }
        public async Task HandleAsync(ErrorLogEvent @event, CancellationToken cancellationToken = default)
        {
            _service.AddLog(@event);
        }
    }
}
