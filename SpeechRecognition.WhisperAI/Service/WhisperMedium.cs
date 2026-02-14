using System;
using System.Collections.Generic;
using System.Text;
using SpeechRecognition.WhisperAI.Enum;
using SpeechRecognition.WhisperAI.Templates;

namespace SpeechRecognition.WhisperAI.Service
{
    internal class WhisperMedium : WhisperModel
    {
        public WhisperMedium(TranslationTemplateModel template  ) : base(template ,WhisperModels.Medium )
        {

        }
    }
}
