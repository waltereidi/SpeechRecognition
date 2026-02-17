using SpeechRecognition.CrossCutting.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.FileStorageDomain
{
    public class FileStorageAggregateId : Value<FileStorageAggregateId>
    {
        protected FileStorageAggregateId() { }
        public Guid Value { get; }

        public FileStorageAggregateId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "FileStorageAggregate id cannot be empty");

            Value = value;
        }

        public static implicit operator Guid(FileStorageAggregateId self) => self.Value;

        public static implicit operator FileStorageAggregateId(string value)
            => new FileStorageAggregateId(Guid.Parse(value));

        public override string ToString() => Value.ToString();

    }
}
