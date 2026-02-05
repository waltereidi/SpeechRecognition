using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Dominio.Entidades
{
    public class UploadRequest : Entity
    {
        public FileStorage FileStorage { get; set; }
        public Guid FileStorageId { get; set; }

    }
}
