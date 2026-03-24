using SpeechRecognition.CrossCutting.Framework;
using SpeechRecognition.FileStorageDomain.DomainEvents;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.Json.Serialization;
using static SpeechRecognition.FileStorageDomain.Entidades.FileStorage;


namespace SpeechRecognition.FileStorageDomain.Entidades;

    public class FileStorage : Entity<FileStorageId>
    {
        public FileStorage(Action<object> applier) : base(applier)
        {

        }
        
        public FileStorage() { }
        [JsonIgnore]
        public FileInfo FileInfo { get; set; }
        public string? OriginalFileName { get; set; }
        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.FileStorageAdded e:
                    FileInfo = e.fi;
                    OriginalFileName = e.originalFileName;
                    Id = new FileStorageId(e.fsId);
                    break;

                case Events.CreateFileStorageConversion e:
                    FileInfo = e.fi;
                    Id = e.fsId;
                    break;
            }
        }
        public override void SetId(string id)
        {
            this.Id = new FileStorageId(Guid.Parse(id));
        }


    }
public class FileStorageId : Value<FileStorageId>
{
    protected FileStorageId() { }
    private Guid Value { get; set; }

    public FileStorageId(Guid value)
    {
        if (value == default)
            throw new ArgumentNullException(nameof(value), "User id cannot be empty");

        Value = value;
    }

    public static implicit operator Guid(FileStorageId self) => self.Value;

    public static implicit operator FileStorageId(string value)
        => new FileStorageId(Guid.Parse(value));

    public override string ToString() => Value.ToString();
}

