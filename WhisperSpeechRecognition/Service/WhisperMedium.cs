using System;
using System.Collections.Generic;
using System.Text;
using WhisperSpeechRecognition.Templates;

namespace WhisperSpeechRecognition.Service
{
    public class WhisperMedium : WhisperModel
    {
        public WhisperMedium(TranslationTemplateModel template ) : base(template)
        {
        }
    }
}
