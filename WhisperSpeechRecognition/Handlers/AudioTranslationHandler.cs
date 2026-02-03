using BuildingBlocks.Messaging.Abstractions;
using Shared.Events.WhisperSpeechRecognition;
using WhisperSpeechRecognition.Interfaces;
using WhisperSpeechRecognition.Service;

namespace WhisperSpeechRecognition.Handlers
{
    public class AudioTranslationHandler : IIntegrationEventHandler<AudioTranslationEvent>
    {
        private readonly ILogger<AudioTranslationHandler> _logger;
        private readonly IEventBus _eventBus;
        public AudioTranslationHandler( ILogger<AudioTranslationHandler> logger , IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }
        public async Task HandleAsync( AudioTranslationEvent @event, CancellationToken cancellationToken = default)
        {
            //ITranslateAudioFacade facade = new TranslateAudioFacade(@event.FilePath, @event.FileStorageConversionId, _logger, _eventBus);
        }
    }
}
