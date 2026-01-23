using System;
using System.Collections.Generic;
using System.Text;
using WhisperSpeechRecognition.Enum;
using WhisperSpeechRecognition.Templates;

namespace WhisperSpeechRecognition.Service
{
    internal class WhisperTiny : WhisperModel
    {
        public WhisperTiny(TranslationTemplateModel template  ) : base(template ,WhisperModels.Tiny )
        {

        }
    }
}
