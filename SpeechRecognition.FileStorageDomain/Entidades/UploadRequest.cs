
using SpeechRecognition.CrossCutting.Framework;

namespace SpeechRecognition.FileStorageDomain.Entidades;

    public class UploadRequest : Entity<UploadRequestId>
    {
    public UploadRequest(Action<object> applier) : base(applier)
    {
    }

    public FileStorage FileStorage { get; set; }
        public Guid FileStorageId { get; set; }

    protected override void When(object @event)
    {
        throw new NotImplementedException();
    }
}

public class UploadRequestId : Value<UploadRequestId>
{
    private Guid Value { get; set; }

    public UploadRequestId(Guid value)
    {
        if (value == default)
            throw new ArgumentNullException(nameof(value), "User id cannot be empty");

        Value = value;
    }

    public static implicit operator Guid(UploadRequestId self) => self.Value;
}