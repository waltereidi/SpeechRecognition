using Microsoft.EntityFrameworkCore;
using SpeechRecognition.Infra.Context;
using SpeechRecognition.Infra.Interfaces.UoW;

namespace SpeechRecognition.Infra.UoW
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task<int> CommitAsync()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
