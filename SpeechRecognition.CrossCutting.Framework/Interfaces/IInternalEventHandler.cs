using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.CrossCutting.Framework.Interfaces
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}
