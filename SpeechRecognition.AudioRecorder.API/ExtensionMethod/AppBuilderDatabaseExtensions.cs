using Microsoft.EntityFrameworkCore;
using SpeechRecognition.Infra.Context;

namespace SpeechRecognition.AudioRecorder.Api.ExtensionMethod
{
    public static class AppBuilderDatabaseExtensions
    {
        public static void EnsureDatabase(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<AppDbContext>();

            if (!context.Database.EnsureCreated())
                context.Database.Migrate();
        }
    }
}
