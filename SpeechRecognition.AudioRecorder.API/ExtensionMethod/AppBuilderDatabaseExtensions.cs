using Microsoft.EntityFrameworkCore;
using SpeechRecognition.Infra.Context;

namespace SpeechRecognition.AudioRecorder.Api.ExtensionMethod
{
    public static class AppBuilderDatabaseExtensions
    {
        public static void EnsureDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider
                               .GetRequiredService<AppDbContext>();

            context.Database.Migrate();
        }
    }
}
