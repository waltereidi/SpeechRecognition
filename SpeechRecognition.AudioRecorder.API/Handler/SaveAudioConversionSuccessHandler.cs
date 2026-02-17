using SpeechRecognition.AudioRecorder.Api.Services;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Shared.Events.AudioRecorderApi;
using SpeechRecognition.CrossCutting.Shared.Events.WhisperSpeechRecognition;
using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.FileStorageDomain.Enum;

namespace SpeechRecognition.AudioRecorder.Api.Handler
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
            //try
            //{
            //    var entity = (FileStorageConversion)await _service.Handle(@event);
            //    await _eventBus.PublishAsync(new AudioTranslationEvent
            //    {
            //        Id = Guid.NewGuid(),
            //        FilePath = entity.FileStorage.FileInfo.FullName,
            //        FileStorageConversionId = entity.Id.ToString()
            //    });
            //}
            //catch(Exception ex)
            //{
            //    _logger.LogError(ex, "Error in SaveAudioConversionSuccessHandler");
            //    _rabbitService.AddLog("SaveAudioConversionSuccessHandler" , ex.Message , LogSeverity.Critical);
            //}
        }
    }
}
