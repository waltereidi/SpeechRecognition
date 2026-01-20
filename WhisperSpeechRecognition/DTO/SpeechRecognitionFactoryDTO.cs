using System;
using System.Collections.Generic;
using System.Text;
using WhisperSpeechRecognition.Contracts;
using WhisperSpeechRecognition.Enum;

namespace WhisperSpeechRecognition.DTO
{
    internal class SpeechRecognitionFactoryDTO
    {
        private TranslationContract.Request.GeneralTranslation dto;

        public SpeechRecognitionFactoryDTO(TranslationContract.Request.GeneralTranslation dto)
        {
            this.dto = dto;
        }

        public TranslationTemplates Template { get; set; }
        public WhisperModels Model { get; set; }
        public Stream AudioStream { get; internal set; }
    }
}
