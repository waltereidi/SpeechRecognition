using SpeechRecognition.Application.Services;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Shared.Events.AudioRecorderApi;
using SpeechRecognition.CrossCutting.Shared.Events.Generic;
using SpeechRecognition.FileStorageDomain;
using System.Security.Cryptography;
using static SpeechRecognition.Application.Contracts.FileStorageAggregateContract;

namespace SpeechRecognition.AudioRecorder.Api.Handler
{
    public class SaveAudioTranslationSuccessHandler : IIntegrationEventHandler<SaveAudioTranslationSuccessEvent>
    {
        private readonly ILogger<SaveAudioTranslationSuccessEvent> _logger;
        private readonly FileStorageAggregateApplicationService _service;
        private readonly IEventBus _eventBus;
        public SaveAudioTranslationSuccessHandler(IEventBus eventBus,
            ILogger<SaveAudioTranslationSuccessEvent> logger,
            FileStorageAggregateApplicationService service
            )
        {
            _logger = logger;
            _service = service;
            _eventBus = eventBus;
        }
        public async Task HandleAsync(SaveAudioTranslationSuccessEvent @event, CancellationToken cancellationToken = default)
        {
            try
            {
                V1.SaveAudioTranslationLocal e = new(
                new(Guid.Parse(@event.FileStorageAggregateId)),
                new(Guid.Parse(@event.FileStorageId)),
                @event.Translation,
                @event.TemplateId,
                @event.ModelId,
                true
                );
                await _service.Handle(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing SaveAudioTranslationSuccessHandler for {@event.FileStorageAggregateId}");
                await _eventBus.PublishAsync(new ErrorLogEvent
                {
                    Severity = 1,
                    ErrorMessage = ex.Message,
                    Source = nameof(SaveAudioTranslationSuccessHandler)
                });

            }
        }
    }
}
