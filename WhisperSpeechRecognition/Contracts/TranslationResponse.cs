using SpeechRecognition.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhisperSpeechRecognition.Contracts
{
    public class TranslationResponse
    {
        public string Translation { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public WhisperModels WhisperModel { get; set; }
    }
}
