using BuildingBlocks.Messaging.Abstractions;
using Shared.Events.WhisperSpeechRecognition;

namespace WhisperSpeechRecognition.Handlers
{
    public class AudioTranslationHandler : IIntegrationEventHandler<AudioTranslationEvent>
    {
        private readonly ILogger<AudioTranslationHandler> _logger;
        public Task HandleAsync(AudioTranslationEvent @event, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
