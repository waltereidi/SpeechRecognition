using SpeechRecognition.Application.Services;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Shared.Events.AudioRecorderApi;
using SpeechRecognition.CrossCutting.Shared.Events.Generic;
using System.Runtime.Intrinsics;
using static SpeechRecognition.Application.Contracts.FileStorageAggregateContract;

namespace SpeechRecognition.AudioRecorder.Api.Handler
{

    public class SaveAudioConversionSuccessHandler : IIntegrationEventHandler<SaveAudioConversionSuccessEvent>
    {
        private readonly ILogger<SaveAudioConversionSuccessEvent> _logger;
        private readonly FileStorageAggregateApplicationService _service;
        private readonly IEventBus _eventBus; 
        public SaveAudioConversionSuccessHandler(IEventBus eventBus, 
            ILogger<SaveAudioConversionSuccessEvent> logger,
            FileStorageAggregateApplicationService service             )
        {
            _logger = logger;
            _service = service;
            _eventBus = eventBus;
        }
        public async Task HandleAsync(SaveAudioConversionSuccessEvent @event, CancellationToken cancellationToken = default)
        {
            try
            {
                V1.UpdateConvertedFile e = new(new(Guid.Parse(@event.FileStorageAggregateId)),
                    new FileStream(@event.FileFullPath, FileMode.Open),
                    new DirectoryInfo(Path.GetDirectoryName(@event.FileFullPath) ?? string.Empty),
                    @event.FileName
                );

                await _service.Handle(@event);
            }catch(Exception ex)
            {
                _logger.LogError(ex, $"Error processing SaveAudioConversionSuccessHandler for {@event.FileStorageAggregateId}");
                await _eventBus.PublishAsync(new ErrorLogEvent
                {
                    Severity = 1,
                    ErrorMessage = ex.Message,
                    Source = nameof(SaveAudioConversionSuccessHandler)
                });
            }

        }
    }
}
