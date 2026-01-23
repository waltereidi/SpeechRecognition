using System;
using System.Collections.Generic;
using System.Text;
using WhisperSpeechRecognition.Enum;
using WhisperSpeechRecognition.Templates;

namespace WhisperSpeechRecognition.Service
{
    internal class WhisperSmall : WhisperModel
    {
        public WhisperSmall(TranslationTemplateModel template  ) : base(template ,WhisperModels.Small )
        {

        }
    }
}
