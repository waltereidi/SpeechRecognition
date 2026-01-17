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
        /// <summary>
        /// Preenchido por whisper, para indicar um erro no resultado
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Preenchido pelo usuário, para indicar se a tradução foi aprovada
        /// </summary>
        public bool IsApproved { get; set; }
        public int TranslationTemplateId { get; set; }
        public TranslationTemplate TranslationTemplate { get; set; }
    }
}
