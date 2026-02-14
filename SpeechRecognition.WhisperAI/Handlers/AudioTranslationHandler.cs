using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using MassTransit.RabbitMqTransport;
using SpeechRecognition.CrossCutting.Shared.Events.AudioRecorderApi;
using SpeechRecognition.CrossCutting.Shared.Events.Generic;
using SpeechRecognition.CrossCutting.Shared.Events.WhisperSpeechRecognition;
using SpeechRecognition.WhisperAI.DTO;
using SpeechRecognition.WhisperAI.Enum;
using SpeechRecognition.WhisperAI.Interfaces;
using SpeechRecognition.WhisperAI.Service;

namespace SpeechRecognition.WhisperAI.Handlers
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
            ISpeechRecognitionAbstractFactory factory = new SpeechRecognitionAbstractFactory();
            try
            {
                var fi = new FileInfo(@event.FilePath);
                if (!fi.Exists)
                    throw new FileNotFoundException("Audio file not found", @event.FilePath);

                var factoryDTO = new SpeechRecognitionFactoryDTO(@event, fi);

                var strategy = await factory.Create(factoryDTO);
                var translation = await strategy.TranslateAudio();

                var publishEvent = CreateSuccessEvent(translation, @event);
                await _eventBus.PublishAsync(publishEvent);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error processing AudioTranslationEvent for FileStorageConversionId: {FileStorageConversionId}", @event.FileStorageConversionId);
                await _eventBus.PublishAsync(new Shared.Events.Generic.ErrorLogEvent()
                {
                    ErrorMessage = ex.Message,
                    Severity = 5,
                    Source = nameof(AudioTranslationHandler)
                });
            }
        }
        private SaveAudioTranslationSuccessEvent CreateSuccessEvent(ITranslationResponseAdapter adapter , AudioTranslationEvent @event  )
        {
            var result = new SaveAudioTranslationSuccessEvent
            {
                FileStorageConversionId = @event.FileStorageConversionId,
                Translation = adapter.GetTranslation(), 
                ModelId = adapter.GetModel(), 
            };
            return result;
        }
    }
}
