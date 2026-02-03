using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Dominio.Entidades
{
    public class FileStorageConversion : Entity
    {
        public Guid FileStorageId { get; set; }
        public FileStorage FileStorage { get; set; }
    }
}
