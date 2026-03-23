using SpeechRecognition.FileStorageDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Application.Models
{
    public class Queries
    {
        public static ReadModels.GetAggregate Query(IEnumerable<FileStorageAggregate> aggs,
            FileStorageAggregateId id
            )
            => new(aggs.FirstOrDefault(x => x.Id.Value == id.Value ));
    }
}
