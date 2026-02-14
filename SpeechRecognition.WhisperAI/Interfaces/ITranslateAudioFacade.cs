using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.WhisperAI.Interfaces
{
    public interface ITranslateAudioFacade
    {
        Task<ITranslationResponseAdapter> TranslateAudio();
    }
}
