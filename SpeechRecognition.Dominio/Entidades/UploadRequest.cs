using SpeechRecognition.Dominio.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Dominio.Entidades
{
    public class UploadRequest : EntityBase<Guid>
    {
        public FileStorage FileStorage { get; set; }
        public Guid FileStorageId { get; set; }

    }
}
