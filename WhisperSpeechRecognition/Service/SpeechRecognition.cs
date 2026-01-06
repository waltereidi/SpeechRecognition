using System;
using System.Collections.Generic;
using System.Text;
using WhisperSpeechRecognition.Interfaces;

namespace WhisperSpeechRecognition.Service
{
    public class SpeechRecognition : ISpeechRecognition
    {
        public SpeechRecognition(Stream file) 
        { 
            
        }
        public string GetTranslation()
        {
            throw new NotImplementedException();
        }
    }
}
