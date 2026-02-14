using SpeechRecognition.CrossCutting.Shared.Events.Generic;
using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.FileStorageDomain.Enum;
using SpeechRecognition.Infra.Context;

namespace SpeechRecognition.AudioRecorder.Api.Services
{
    public class RabbitMqLogService
    {
        private readonly AppDbContext _context;
        public RabbitMqLogService(AppDbContext context)
        {
            _context = context;
        }
        public void AddLog(ErrorLogEvent @event)
        {
            var logs = new RabbitMqLog()
            {
                Severity = (LogSeverity)@event.Severity,
                Description = @event.ErrorMessage,
                Source = @event.Source
            };
            _context.RabbitMqLogs.Add(logs);
            _context.SaveChangesAsync();
        }
        public void AddLog(string source, string message , LogSeverity severity)
        {
            var logs = new RabbitMqLog()
            {
                Severity = severity,
                Description =message,
                Source = source
            };
            _context.RabbitMqLogs.Add(logs);
            _context.SaveChangesAsync();
        }
    }
}
