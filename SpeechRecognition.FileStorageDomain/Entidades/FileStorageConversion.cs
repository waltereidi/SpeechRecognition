

using SpeechRecognition.CrossCutting.Framework;
using SpeechRecognition.FileStorageDomain.DomainEvents;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static SpeechRecognition.FileStorageDomain.Entidades.FileStorage;

namespace SpeechRecognition.FileStorageDomain.Entidades;
public class FileStorageConversion : Entity<FileStorageConversionId>
{
    [JsonIgnore]
    [Required]
    public FileStorageId FileStorageId { get; set; }
    public FileStorageConversion()
    {

    }

    public FileStorageConversion(Action<object> applier) : base(applier)
    {
    }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case Events.CreateFileStorageConversion e:
                Id = new FileStorageConversionId(Guid.NewGuid());
                FileStorageId = e.fsId;
                break;
        }
    }

    public override void SetId(string id)
    {
        Id = new FileStorageConversionId(Guid.Parse(id));
    }
}

public class FileStorageConversionId : Value<FileStorageConversionId>
{
    protected FileStorageConversionId() { }
    private Guid Value { get; set; }

    public FileStorageConversionId(Guid value)
    {
        if (value == default)
            throw new ArgumentNullException(nameof(value), "User id cannot be empty");

        Value = value;
    }

    public static implicit operator Guid(FileStorageConversionId self) => self.Value;

    public static implicit operator FileStorageConversionId(string value)
        => new FileStorageConversionId(Guid.Parse(value));

    public override string ToString() => Value.ToString();
}