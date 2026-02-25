using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.CrossCutting.Framework
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException(Type type, string message)
            : base($"Value of {type.Name} {message}")
        {
        }
    }
}
