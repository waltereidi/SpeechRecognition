using BuildingBlocks.Messaging.Abstractions;
using Shared.Events.AudioRecorderApi;

namespace AudioRecorder.Api.Handler
{
    public class SaveAudioConversionSuccessHandler: IIntegrationEventHandler<SaveAudioConversionSuccessEvent>
    { 
        private readonly ILogger<SaveAudioConversionSuccessEvent> _logger;
        public SaveAudioConversionSuccessHandler(IEventBus eventBus, ILogger<SaveAudioConversionSuccessEvent> logger)
        {
            _logger = logger;
        }
        public async Task HandleAsync(SaveAudioConversionSuccessEvent @event, CancellationToken cancellationToken = default)
        {

        }
    }
}
