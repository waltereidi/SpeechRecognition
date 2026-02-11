using SpeechRecognition.Dominio.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Dominio.Entidades
{
    public class FileStorage : EntityBase<Guid>
    {
        public FileInfo FileInfo { get; set; }
        public string OriginalFileName { get; set; }
    }
}
