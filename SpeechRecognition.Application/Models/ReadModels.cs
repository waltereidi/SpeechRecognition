using SpeechRecognition.FileStorageDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Application.Models
{
    public class ReadModels
    {
        public record GetAggregate(FileStorageAggregate agg); 
    }
}
