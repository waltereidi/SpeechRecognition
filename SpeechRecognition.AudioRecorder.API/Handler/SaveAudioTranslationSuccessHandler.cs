using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Shared.Events.AudioRecorderApi;

namespace SpeechRecognition.AudioRecorder.Api.Handler
{
    public class SaveAudioTranslationSuccessHandler : IIntegrationEventHandler<SaveAudioTranslationSuccessEvent>
    {
        private readonly ILogger<SaveAudioTranslationSuccessEvent> _logger;
        public SaveAudioTranslationSuccessHandler(IEventBus eventBus, ILogger<SaveAudioTranslationSuccessEvent> logger)
        {
            _logger = logger;
        }
        public async Task HandleAsync(SaveAudioTranslationSuccessEvent @event, CancellationToken cancellationToken = default)
        {

        }
    }
}
