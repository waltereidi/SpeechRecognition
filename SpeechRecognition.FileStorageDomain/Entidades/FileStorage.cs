using SpeechRecognition.CrossCutting.Framework;

namespace SpeechRecognition.FileStorageDomain.Entidades;

public class FileStorage : Entity<FileStorageId>
{
    public FileStorage(Action<object> applier) : base(applier)
    {
    }

    public FileInfo FileInfo { get; set; }
    public string OriginalFileName { get; set; }
    public FileStorageAggregateId FileStorageAggregateId { get; set; }
    protected override void When(object @event)
    {
        throw new NotImplementedException();
    }
}
public class FileStorageId : Value<FileStorageId>
{
    private Guid Value { get; set; }

    public FileStorageId(Guid value)
    {
        if (value == default)
            throw new ArgumentNullException(nameof(value), "User id cannot be empty");

        Value = value;
    }

    public static implicit operator Guid(FileStorageId self) => self.Value;
}
