using BuildBlocksRabbitMq.Events.AudioConverter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AudioConverter.EventHandlers
{
    public sealed class RequestAudioConversionHandler
    {
        private readonly ILogger<RequestAudioConversionHandler> _logger;

        public RequestAudioConversionHandler(ILogger<RequestAudioConversionHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(RequestAudioConverterToWav notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "🚀 [CartsModule] Recebido evento UserRegisteredEvent para usuário {UserId}. Preparando para criar carrinho...",
                notification.EventId);

            return Task.CompletedTask;
        }
    }
}
