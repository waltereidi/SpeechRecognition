using System;
using System.Collections.Generic;
using System.Text;
using WhisperSpeechRecognition.Enum;

namespace WhisperSpeechRecognition.DTO
{
    internal class SpeechRecognitionFactoryDTO
    {
        public TranslationTemplates Template { get; set; }
        public WhisperModels Model { get; set; }
    }
}
