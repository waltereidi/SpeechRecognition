using AudioConverter.Interfaces;
using AudioConverter.Services.Windows;
using BuildingBlocks.Messaging.Abstractions;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AudioConverter.Handlers
{
    public class AudioConversionToWav16kLocalHandler : IIntegrationEventHandler<AudioConversionToWav16kLocalEvent>
    {
        private readonly IEventBus _eventBus;
        
        public AudioConversionToWav16kLocalHandler()
        {
        }
        private readonly ILogger<AudioConversionToWav16kLocalHandler> _logger;
        public async Task HandleAsync(AudioConversionToWav16kLocalEvent @event, CancellationToken cancellationToken = default)
        {


        }
    }
}
