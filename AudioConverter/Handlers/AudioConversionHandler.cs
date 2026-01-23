using BuildingBlocks.Messaging.Abstractions;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace AudioConverter.Handlers
{
    public class AudioConversionHandler : IIntegrationEventHandler<AudioConversionEvent>
    {
        private readonly ILogger<AudioConversionEvent> _logger;
        public Task HandleAsync(AudioConversionEvent @event, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
