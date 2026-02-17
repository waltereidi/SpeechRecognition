using SpeechRecognition.CrossCutting.Framework;
using SpeechRecognition.FileStorageDomain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SpeechRecognition.FileStorageDomain.Entidades;

public class RabbitMqLog : Entity<RabbitMqLogId>
{
    public RabbitMqLog(Action<object> applier) : base(applier)
    {
    }

    public string Description { get; set; }
    public LogSeverity Severity { get; set; }
    [Required]
    public FileStorageAggregateId FileStorageAggregateId { get; set; }
    protected override void When(object @event)
    {
        throw new NotImplementedException();
    }
}

public class RabbitMqLogId : Value<RabbitMqLogId>
{
    private Guid Value { get; set; }

    public RabbitMqLogId(Guid value)
    {
        if (value == default)
            throw new ArgumentNullException(nameof(value), "User id cannot be empty");

        Value = value;
    }

    public static implicit operator Guid(RabbitMqLogId self) => self.Value;
}