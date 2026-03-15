using SpeechRecognition.CrossCutting.Framework.Interfaces;
using SpeechRecognition.Infra.Context;

namespace SpeechRecognition.Infra.UoW
{
    public class FireStoreUnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task CommitAsync()
            => await context.SaveChangesAsync();
    }
}
