using Google.Cloud.Firestore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;

namespace SpeechRecognition.Infra.Firestore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFirestore(this IServiceCollection services, IConfiguration configuration)
        {
            // espera configuração:
            // "Firestore": { "ProjectId": "...", "EmulatorUrl": "optional" }
            var projectId = configuration["Firestore:ProjectId"];
            if (string.IsNullOrWhiteSpace(projectId))
                throw new ArgumentException("Firestore:ProjectId não configurado.");

            // Suporta emulador via FIRESTORE_EMULATOR_HOST env var ou configuração
            var emulator = configuration["Firestore:EmulatorUrl"];
            if (!string.IsNullOrWhiteSpace(emulator))
            {
                Environment.SetEnvironmentVariable("FIRESTORE_EMULATOR_HOST", emulator);
            }

            var db = FirestoreDb.Create(projectId);
            services.AddSingleton(db);
            return services;
        }
    }
}