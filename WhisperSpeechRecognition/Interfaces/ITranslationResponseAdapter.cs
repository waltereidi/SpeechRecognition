
using System;
using System.Collections.Generic;
using System.Text;

namespace WhisperSpeechRecognition.Interfaces
{
    public interface ITranslationResponseAdapter
    {
        public int GetModel();
        public string GetTranslation();
        public string GetJson();
        public byte[] GetJsonBytea();
    }
}
