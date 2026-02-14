using SpeechRecognition.AudioRecorder.Api.Services;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Shared.Events.AudioRecorderApi;

namespace SpeechRecognition.AudioRecorder.Api.Handler
{
    public class SaveAudioTranslationSuccessHandler : IIntegrationEventHandler<SaveAudioTranslationSuccessEvent>
    {
        private readonly ILogger<SaveAudioTranslationSuccessEvent> _logger;
        private readonly AudioTranlsationService _service;
        private readonly RabbitMqLogService _rabbitService;
        private readonly IEventBus _eventBus;
        public SaveAudioTranslationSuccessHandler(IEventBus eventBus, 
            ILogger<SaveAudioTranslationSuccessEvent> logger, 
            AudioTranlsationService service, 
            RabbitMqLogService rabbitService)
        {
            _logger = logger;
            _service = service;
            _rabbitService = rabbitService;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(SaveAudioTranslationSuccessEvent @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await _service.SaveAudioTranslation(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveAudioTranslationSuccessHandler");
                _rabbitService.AddLog(new Shared.Events.Generic.ErrorLogEvent()
                {
                    ErrorMessage = ex.Message,
                    Severity = 5,
                    Source = nameof(SaveAudioTranslationSuccessHandler)
                });
            }

        }
    }
}
