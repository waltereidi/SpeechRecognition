using SpeechRecognition.CrossCutting.Framework;
using SpeechRecognition.FileStorageDomain.Enum;

namespace SpeechRecognition.FileStorageDomain.Entidades;

    public class RabbitMqLog : Entity<RabbitMqLogId>
    {
    public RabbitMqLog(Action<object> applier) : base(applier)
    {
    }

    public string Description { get; set; }
        /// <summary>
        /// Severity level of the error (e.g., 1-5)
        /// 1 - Critical
        /// 2 - High
        /// 3 - Medium
        /// 4 - Low
        /// 5 - Informational
        /// </summary>
        public LogSeverity Severity { get; set; }
        public string Source { get; set; }

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