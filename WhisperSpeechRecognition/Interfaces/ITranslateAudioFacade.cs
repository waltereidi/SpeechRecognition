using SpeechRecognition.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhisperSpeechRecognition.Interfaces
{
    public interface ITranslateAudioFacade
    {
        Task<string> TranslateAudioLocal(AudioTranslation entity); 
    }
}
