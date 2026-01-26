using BuildingBlocks.Messaging.Abstractions;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AudioConverter.Handlers
{
    public class AudioConversionHandler : IIntegrationEventHandler<AudioConversionEvent>
    {
        private readonly ILogger<AudioConversionEvent> _logger;
        public async Task HandleAsync(AudioConversionEvent @event, CancellationToken cancellationToken = default)
        {

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("Running on Windows");
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
