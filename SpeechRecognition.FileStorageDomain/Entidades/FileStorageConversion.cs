

using SpeechRecognition.CrossCutting.Framework;
using System.ComponentModel.DataAnnotations;

namespace SpeechRecognition.FileStorageDomain.Entidades;
public class FileStorageConversion : Entity<FileStorageConversionId>
{
    [Required]
    public FileStorageId FileStorageId { get; set; }
    [Required]
    public FileStorageAggregateId FileStorageAggregateId { get; set; }

    public FileStorageConversion(Action<object> applier) : base(applier)
    {
    }

    protected override void When(object @event)
    {
        throw new NotImplementedException();
    }
}

public class FileStorageConversionId : Value<FileStorageConversionId>
{
    private Guid Value { get; set; }

    public FileStorageConversionId(Guid value)
    {
        if (value == default)
            throw new ArgumentNullException(nameof(value), "User id cannot be empty");

        Value = value;
    }

    public static implicit operator Guid(FileStorageConversionId self) => self.Value;
}