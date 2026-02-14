using System;
using System.Collections.Generic;
using System.Text;
using SpeechRecognition.WhisperAI.Interfaces;

namespace SpeechRecognition.WhisperAI.Contracts
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
                private bool _operationIsSuccessfull;
                private string _errorMessage;
                public GeneralTranslation(int model , string translation)
                {
                    _model = model;
                    _translation = translation;
                    _operationIsSuccessfull = true;
                }
                public GeneralTranslation(string errorMessage)
                {
                    _operationIsSuccessfull = false;
                    _errorMessage = errorMessage;
                }

                public string GetJson()
                {
                   object result = new
                   {
                       model = GetModel(),
                       translation = GetTranslation(),
                       operationIsSuccessfull = _operationIsSuccessfull,
                       errorMessage = _errorMessage,
                       createdAt = DateTime.UtcNow
                   };
                    return System.Text.Json.JsonSerializer.Serialize(result);
                }

                public byte[] GetJsonBytea()
                {
                    var json = GetJson();
                    return Encoding.UTF8.GetBytes(json);
                }

                public int GetModel() => _model; 
                public string GetTranslation() => _translation;
            }
        }
    }
}
