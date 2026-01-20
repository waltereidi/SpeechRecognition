using System;
using System.Collections.Generic;
using System.Text;
using WhisperSpeechRecognition.Interfaces;

namespace WhisperSpeechRecognition.Contracts
{
    public class TranslationContract
    {
        public class Request 
        {
            public record GeneralTranslation(Stream filePath , int whisperModel );
        }
        public class Response
        {
            public class GeneralTranslation : ITranslationResponseAdapter
            {
                private int _model; 
                private string _translation;
                public GeneralTranslation(int model , string translation )
                {
                    _model = model;
                    _translation = translation;
                }
                public int GetModel() => _model; 
                public string GetTranslation() => _translation;
            }
        }
    }
}
