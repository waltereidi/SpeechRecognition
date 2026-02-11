using SpeechRecognition.Dominio.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Dominio.Entidades
{
    public class AudioTranslation : EntityBase<Guid>
    {
        public string Translation { get; set; }
        public Guid FileStorageId { get; set; }
        public FileStorage FileStorage { get; set; }
        /// <summary>
        /// Preenchido por whisper, para indicar um erro no resultado
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Preenchido pelo usuário, para indicar se a tradução foi aprovada
        /// </summary>
        public bool? IsApproved { get; set; }
        public int TranslationTemplate { get; set; }
        public int WhisperModel { get; set; }
    }
}
