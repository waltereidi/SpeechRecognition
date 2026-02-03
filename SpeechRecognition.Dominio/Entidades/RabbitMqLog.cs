using SpeechRecognition.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Dominio.Entidades
{
    public class RabbitMqLog : Entity
    {
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
    }
}
