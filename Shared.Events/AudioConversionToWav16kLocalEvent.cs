using BuildingBlocks.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public  record class AudioConversionToWav16kLocalEvent : IntegrationEvent
    {
        public string DirectoryPath { get; init; }
        public string FilePath { get; init; }
    }
    
}
