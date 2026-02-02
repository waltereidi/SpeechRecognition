using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Dominio.Entidades
{
    public class RabbitMqLog : Entity
    {
        public string Description { get; set; }
        public int Severity { get; set; }
        public string Source { get; set; }
    }
}
