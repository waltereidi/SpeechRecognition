using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SpeechRecognition.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhisperSpeechRecognition.Contracts
{
    public class TranslationResponse
    {
        public string Translation { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public WhisperModels WhisperModel { get; set; }
        public TranslationResponse(string result, WhisperModels model )
        {
            Translation = result;
            IsSuccess = true;
            WhisperModel = model;
        }
        public TranslationResponse(Exception ex  )
        {
            Translation = string.Empty;
            IsSuccess = false;
            ErrorMessage = ex.Message;
            WhisperModel = WhisperModels.None;
        }
    }
}
