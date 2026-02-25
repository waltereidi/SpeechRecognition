using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace SpeechRecognition.CrossCutting.IoC
{
    public static class RegisterIoC
    {
        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
        {
            //#region Repositories

            ////services.AddScoped<IRepositoryFileStorage, RepositoryFileStorage>();
            ////services.AddScoped<IRepositoryFileStorageConversion, RepositoryFileStorageConversion>();
            ////services.AddScoped<IRepositoryUploadRequest, RepositoryUploadRequest>();
            ////services.AddScoped<IRepositoryRabbitMqLog , RepositoryRabbitMqLog>();
            ////services.AddScoped<IRepositoryAudioTranslation, RepositoryAudioTranslation>();

            //#endregion

            //#region Application Services

            ////services.AddScoped<AudioConversionService>();
            ////services.AddScoped<RabbitMqLogService>();

            //#endregion

            //#region Unit of Work

            //services.AddScoped<IUnitOfWork, PostgresqlUnitOfWork>();

            //#endregion

            //#region Context    

            //services.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseNpgsql(configuration.GetConnectionString("ConnectionString"));
            //});

            //#endregion

            return services;
        }
    }
}
