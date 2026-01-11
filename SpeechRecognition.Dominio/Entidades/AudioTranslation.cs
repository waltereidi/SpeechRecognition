using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Dominio.Entidades
{
    public class AudioTranslation : Entity
    {
        public string Translation { get; set; }
        public int FileStorageId { get; set; }
        public FileStorage FileStorage { get; set; }
    }
}
