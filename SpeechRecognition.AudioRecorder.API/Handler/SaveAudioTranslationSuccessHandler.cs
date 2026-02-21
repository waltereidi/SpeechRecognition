using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Shared.Events.AudioRecorderApi;
using SpeechRecognition.CrossCutting.Shared.Events.Generic;

namespace SpeechRecognition.AudioRecorder.Api.Handler
{
    public class SaveAudioTranslationSuccessHandler : IIntegrationEventHandler<SaveAudioTranslationSuccessEvent>
    {
        private readonly ILogger<SaveAudioTranslationSuccessEvent> _logger;
        private readonly IEventBus _eventBus;
        public SaveAudioTranslationSuccessHandler(IEventBus eventBus, 
            ILogger<SaveAudioTranslationSuccessEvent> logger )
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(SaveAudioTranslationSuccessEvent @event, CancellationToken cancellationToken = default)
        {
            //try
            //{
            //    await _service.SaveAudioTranslation(@event);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error in SaveAudioTranslationSuccessHandler");
            //    _rabbitService.AddLog(new ErrorLogEvent()
            //    {
            //        ErrorMessage = ex.Message,
            //        Severity = 5,
            //        Source = nameof(SaveAudioTranslationSuccessHandler)
            //    });
            //}

        }
    }
}
