using SpeechRecognition.Application.Interfaces;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Framework.Interfaces;
using SpeechRecognition.FileStorageDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Application.Models
{
    public class Queries
    {
        private readonly IFileStorageAggregateRepository _repository;

        public Queries( IFileStorageAggregateRepository repository )
        {
            _repository = repository;
        }
        public async Task<ReadModels.GetAggregate> GetAggregate( FileStorageAggregateId id )
        {
            var result = await _repository.GetByAsync(id);
            return new(result );
        }
            
    }
}
