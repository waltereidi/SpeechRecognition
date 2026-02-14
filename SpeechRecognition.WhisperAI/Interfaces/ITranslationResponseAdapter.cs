
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.WhisperAI.Interfaces
{
    public interface ITranslationResponseAdapter
    {
        public int GetModel();
        public string GetTranslation();
        public string GetJson();
        public byte[] GetJsonBytea();
    }
}
