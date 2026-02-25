using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.CrossCutting.Framework.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
