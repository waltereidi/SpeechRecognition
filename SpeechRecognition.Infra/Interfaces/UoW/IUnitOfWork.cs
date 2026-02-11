using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.Interfaces.UoW
{

    public interface IUnitOfWork
    {
        Task<int> CommitAsync();    
    }
}
