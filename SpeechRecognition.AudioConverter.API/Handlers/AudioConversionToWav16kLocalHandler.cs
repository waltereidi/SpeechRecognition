using SpeechRecognition.AudioConverter.Api.Interfaces;
using SpeechRecognition.AudioConverter.Api.Services;
using SpeechRecognition.AudioConverter.Api.Services.Windows;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Shared.Events.AudioConverter;
using SpeechRecognition.CrossCutting.Shared.Events.AudioRecorderApi;
using SpeechRecognition.CrossCutting.Shared.Events.Generic;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SpeechRecognition.AudioConverter.Api.Handlers
{
    public class AudioConversionToWav16kLocalHandler : IIntegrationEventHandler<AudioConversionToWav16kLocalEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<AudioConversionToWav16kLocalHandler> _logger;
        private readonly IConfiguration _config;
        public AudioConversionToWav16kLocalHandler(IEventBus eventBus, ILogger<AudioConversionToWav16kLocalHandler> logger, IConfiguration config)
        {
            _eventBus = eventBus;
            _logger = logger;
            _config = config;
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
                    Id= Guid.Parse(@event.FileStorageId),  
                }, cancellationToken);

            }
            catch (Exception ex)
            {
                await _eventBus.PublishAsync(new ErrorLogEvent
                {
                    Severity = 5,
                    ErrorMessage = ex.Message,
                    Source = nameof(AudioConversionToWav16kLocalHandler)
                }, cancellationToken);
                //_logger.LogError(ex, "Error handling AudioConversionToWav16kLocalEvent");
            }
        }
    }
}
