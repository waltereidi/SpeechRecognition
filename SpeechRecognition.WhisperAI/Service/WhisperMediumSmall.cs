using System;
using System.Collections.Generic;
using System.Text;
using SpeechRecognition.WhisperAI.Enum;
using SpeechRecognition.WhisperAI.Templates;
using Whisper.net;

namespace SpeechRecognition.WhisperAI.Service
{
    internal class WhisperSmall : WhisperModel
    {
        public WhisperSmall(TranslationTemplateModel template  ) : base(template ,WhisperModels.Small)
        {

        }
    }
}
