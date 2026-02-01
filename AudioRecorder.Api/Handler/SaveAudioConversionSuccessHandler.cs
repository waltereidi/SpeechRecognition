using BuildingBlocks.Messaging.Abstractions;
using Shared.Events.AudioRecorderApi;

namespace AudioRecorder.Api.Interfaces
{
    public class SaveAudioConversionErrorHandler : IIntegrationEventHandler<SaveAudioConversionErrorEvent>
    { 
        private readonly ILogger<SaveAudioConversionErrorEvent> _logger;
        public SaveAudioConversionErrorHandler(IEventBus eventBus, ILogger<SaveAudioConversionErrorEvent> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(SaveAudioConversionErrorEvent @event, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
