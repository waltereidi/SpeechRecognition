using SpeechRecognition.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhisperSpeechRecognition.Interfaces
{
    public interface ITranslationResponseAdapter
    {
        public WhisperModels GetModel();
        public string GetTranslation();
    }
}
