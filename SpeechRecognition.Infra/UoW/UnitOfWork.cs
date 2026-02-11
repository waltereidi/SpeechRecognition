using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public async Task<int> CommitAsync()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                AddNotification("Database", ex.InnerException == null ? ex.Message : $"{ex.Message} - {ex.InnerException.Message}");
                return 0;
            }
        }
    }
}
