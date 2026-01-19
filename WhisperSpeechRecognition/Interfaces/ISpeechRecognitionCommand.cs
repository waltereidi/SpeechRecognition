
using System;
using System.Collections.Generic;
using System.Text;
using WhisperSpeechRecognition.Contracts;
using WhisperSpeechRecognition.DTO;
using WhisperSpeechRecognition.Service;

namespace WhisperSpeechRecognition.Interfaces
{
    internal interface ISpeechRecognitionAbstractFactory
    {
        public Task<ITranslateAudioFacade> Create(SpeechRecognitionFactoryDTO dto);
    }
}
