using System;
using System.Collections.Generic;
using System.Text;

namespace WhisperSpeechRecognition.Interfaces
{
    public interface ITranslateAudioFacade
    {
        Task<ITranslationResponseAdapter> TranslateAudio(AudioTranslation entity);
    }
}
