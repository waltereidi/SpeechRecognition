using System;
using System.Collections.Generic;
using System.Text;

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
            public record GeneralTranslation(string translation , bool isSuccess , string errorMessage);
        }
    }
}
