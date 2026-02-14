using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.WhisperAI.Templates
{
    public class GeneralTranslation : TranslationTemplateModel
    {
        public override string GetResult()
            => string.Join(" ", _segments.ConvertAll(s => s.Text));

    }
}
