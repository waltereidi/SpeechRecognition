using AudioConverter.Interfaces;
using AudioConverter.Services;
using AudioConverter.Services.Windows;
using BuildingBlocks.Messaging.Abstractions;
using Shared.Events.AudioConverter;
using Shared.Events.AudioRecorderApi;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AudioConverter.Handlers
{
    public class AudioConversionToWav16kLocalHandler : IIntegrationEventHandler<AudioConversionToWav16kLocalEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<AudioConversionToWav16kLocalHandler> _logger;

        public AudioConversionToWav16kLocalHandler(IEventBus eventBus, ILogger<AudioConversionToWav16kLocalHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task HandleAsync(AudioConversionToWav16kLocalEvent @event, CancellationToken cancellationToken = default)
        {
            try
            {
                AudioConversionFactory factory = new(@event.DirectoryPath, @event.FilePath);
                var strategy = factory.GetStrategy();
                await strategy.Start(cancellationToken);
                
                var adapter = factory.GetAdapter();
                
                await _eventBus.PublishAsync(new SaveAudioConversionSuccessEvent
                {
                    FileFullPath= adapter.GetResultFileInfo().FullName,
                    FileName= adapter.GetResultFileName(),
                    Id= @event.FileStorageId
                }, cancellationToken);

            }
            catch (Exception ex)
            {
                await _eventBus.PublishAsync(new SaveAudioConversionErrorEvent
                {
                    FileStorageId = @event.FileStorageId,
                    ErrorMessage = ex.Message
                }, cancellationToken);
                _logger.LogError(ex, "Error handling AudioConversionToWav16kLocalEvent");
            }
        }
    }
}
