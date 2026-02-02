using Shared.Events.Generic;
using SpeechRecognition.Dominio.Entidades;
using SpeechRecognition.Infra.Context;

namespace AudioRecorder.Api.Services
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
                Severity = @event.Severity,
                Description = @event.ErrorMessage,
                Source = @event.Source
            };
            _context.RabbitMqLogs.Add(logs);
            _context.SaveChangesAsync();
        }
    }
}
