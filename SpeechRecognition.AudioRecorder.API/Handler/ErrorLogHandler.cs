using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Shared.Events.Generic;

namespace SpeechRecognition.AudioRecorder.Api.Interfaces
{
    public class RabbitMqLogHandler : IIntegrationEventHandler<ErrorLogEvent>
    { 
        private readonly ILogger<ErrorLogEvent> _logger;
        public RabbitMqLogHandler(IEventBus eventBus, ILogger<ErrorLogEvent> logger  )
        {
            _logger = logger;
        }
        public async Task HandleAsync(ErrorLogEvent @event, CancellationToken cancellationToken = default)
        {
        }
    }
}
