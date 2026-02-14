using SpeechRecognition.CrossCutting.Shared.Events.WhisperSpeechRecognition;
using System;
using System.Collections.Generic;
using System.Text;
using SpeechRecognition.WhisperAI.Contracts;
using SpeechRecognition.WhisperAI.Enum;

namespace SpeechRecognition.WhisperAI.DTO
{
    internal class SpeechRecognitionFactoryDTO
    {
        private TranslationContract.Request.GeneralTranslation dto;

        public SpeechRecognitionFactoryDTO(TranslationContract.Request.GeneralTranslation dto)
        {
            this.dto = dto;
            this.AudioStream = dto.filePath;
            this.Model = (WhisperModels)dto.whisperModel;
        }

        public SpeechRecognitionFactoryDTO(AudioTranslationEvent @event , FileInfo fi  )
        {
            Template = (TranslationTemplates)@event.TemplateId;
            AudioStream = fi.OpenRead();
            Model = @event.ModelId != null ?
                (WhisperModels)@event.ModelId
                : WhisperModels.None;
        }
        public TranslationTemplates Template { get; set; }
        public WhisperModels Model { get; set; }
        public Stream AudioStream { get; internal set; }
    }
}
