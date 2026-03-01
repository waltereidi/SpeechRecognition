using SpeechRecognition.CrossCutting.Framework;
using SpeechRecognition.FileStorageDomain.DomainEvents;
using SpeechRecognition.FileStorageDomain.Enum;
using System.ComponentModel.DataAnnotations;
using System.Formats.Tar;

namespace SpeechRecognition.FileStorageDomain.Entidades;

public class RabbitMqLog : Entity<RabbitMqLogId>
{
    protected RabbitMqLog()
    {
    }   
    public RabbitMqLog(Action<object> applier) : base(applier)
    {
    }

    public string Description { get; set; }
    public LogSeverity Severity { get; set; }
    protected override void When(object @event)
    {
        switch (@event)
        {
            case Events.ErrorLog e:
                Description = $"{e.source}{e.errorMessage}";
                Severity = (LogSeverity)e.severity;
                break;
        }
    }
}

public class RabbitMqLogId : Value<RabbitMqLogId>
{
    protected RabbitMqLogId() { }
    private Guid Value { get; set; }

    public RabbitMqLogId(Guid value)
    {
        if (value == default)
            throw new ArgumentNullException(nameof(value), "User id cannot be empty");

        Value = value;
    }

    public static implicit operator Guid(RabbitMqLogId self) => self.Value;
}