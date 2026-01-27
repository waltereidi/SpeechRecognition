using AudioConverter.Interfaces;
using BuildingBlocks.Messaging.Abstractions;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AudioConverter.Handlers
{
    public class AudioConversionToWav16kHandler : IIntegrationEventHandler<AudioConversionToWav16kEvent>
    {
        private readonly ILogger<AudioConversionToWav16kHandler> _logger;
        public async Task HandleAsync(AudioConversionToWav16kEvent @event, CancellationToken cancellationToken = default)
        {

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Console.WriteLine("Running on Windows");
            }
            else
            {
                throw new Exception("This implementation does not contains resolution for the current running operational system");
            }

        }
    }
}
