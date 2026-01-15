using System;
using System.Collections.Generic;
using System.Text;

namespace WhisperSpeechRecognition.Templates
{
    public class GeneralTranslation : TranslationTemplate
    {
        public override string GetResult()
            => string.Join(" ", _segments.ConvertAll(s => s.Text));

    }
}
