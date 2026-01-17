using SpeechRecognition.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using WhisperSpeechRecognition.Service;

namespace WhisperSpeechRecognition.Interfaces
{
    internal interface ISpeechRecognitionAbstractFactory
    {
        public Task<ISpeechRecognitionStrategy> Create(AudioTranslation fsc);
    }
}
