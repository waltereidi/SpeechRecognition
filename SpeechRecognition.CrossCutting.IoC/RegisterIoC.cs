using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpeechRecognition.Application.Models;
using SpeechRecognition.Application.Services;


namespace SpeechRecognition.CrossCutting.IoC
{
    public static class RegisterIoC
    {
        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
        {

            //#region Unit of Work
            services.AddScoped<FileStorageAggregateApplicationService>();
            services.AddScoped<Queries>();

            return services;
        }
    }
}
