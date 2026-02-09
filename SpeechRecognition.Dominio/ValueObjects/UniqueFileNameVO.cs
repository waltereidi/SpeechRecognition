using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Dominio.ValueObjects
{
    public class UniqueFileNameVO
    {
        public string Value { get; }
        public UniqueFileNameVO(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

            var extension = Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(extension))
                throw new ArgumentException("File name must have an extension.", nameof(fileName));

            Value = $"{Guid.NewGuid()}{extension}";
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj)
            => obj is UniqueFileNameVO other && Value == other.Value;
    }
}
