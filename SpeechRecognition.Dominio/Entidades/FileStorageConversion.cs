using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Dominio.Entidades
{
    public class FileStorageConversion : Entity
    {
        public int FileStorageId { get; set; }
        public FileStorage FileStorage { get; set; }
    }
}
