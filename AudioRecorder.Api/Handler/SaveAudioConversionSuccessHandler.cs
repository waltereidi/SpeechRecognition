using AudioRecorder.Api.Services;
using BuildingBlocks.Messaging.Abstractions;
using Shared.Events.AudioRecorderApi;
using Shared.Events.WhisperSpeechRecognition;
using SpeechRecognition.Dominio.Enum;

namespace AudioRecorder.Api.Handler
{
    public class SaveAudioConversionSuccessHandler: IIntegrationEventHandler<SaveAudioConversionSuccessEvent>
    { 
        private readonly ILogger<SaveAudioConversionSuccessEvent> _logger;
        private readonly AudioConversionService _service;
        private readonly RabbitMqLogService _rabbitService;
        private readonly IEventBus _eventBus;
        public SaveAudioConversionSuccessHandler(IEventBus eventBus, ILogger<SaveAudioConversionSuccessEvent> logger , AudioConversionService service , RabbitMqLogService rabbitService  )
        {
            _logger = logger;
            _service = service;
            _rabbitService = rabbitService;
            _eventBus = eventBus;
        }

        public async Task HandleAsync( SaveAudioConversionSuccessEvent @event, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await _service.SaveAudioConversion(@event);
                await _eventBus.PublishAsync(new AudioTranslationEvent
                {
                    Id = Guid.NewGuid(),
                    FilePath = entity.FileStorage.FileInfo.FullName,
                    FileStorageConversionId = entity.Id.ToString()
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error in SaveAudioConversionSuccessHandler");
                _rabbitService.AddLog("SaveAudioConversionSuccessHandler" , ex.Message , LogSeverity.Critical);
            }
        }
    }
}
