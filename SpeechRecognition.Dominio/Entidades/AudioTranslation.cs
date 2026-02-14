using SpeechRecognition.CrossCutting.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Dominio.Entidades;

    public class AudioTranslation : Entity<AudioTranslationId>
    {
        public AudioTranslation(Action<object> applier) : base(applier)
        {
        }

        public string Translation { get; private set; }
        public Guid FileStorageId { get; private set; }
        public FileStorage FileStorage { get; private set; }
        /// <summary>
        /// Preenchido por whisper, para indicar um erro no resultado
        /// </summary>
        public bool IsSuccess { get; private set; }
        /// <summary>
        /// Preenchido pelo usuário, para indicar se a tradução foi aprovada
        /// </summary>
        public bool? IsApproved { get; private set; }
        public int TranslationTemplate { get; private set; }
        public int WhisperModel { get; private set; }

        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }

    public class AudioTranslationId : Value<AudioTranslationId>
    {
        private Guid Value { get; set; }

        public AudioTranslationId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "User id cannot be empty");

            Value = value;
        }

        public static implicit operator Guid(AudioTranslationId self) => self.Value;
    }
