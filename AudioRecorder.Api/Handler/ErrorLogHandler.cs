using AudioRecorder.Api.Services;
using BuildingBlocks.Messaging.Abstractions;
using Shared.Events.Generic;

namespace AudioRecorder.Api.Interfaces
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
