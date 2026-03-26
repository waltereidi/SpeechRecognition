using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpeechRecognition.Application.Interfaces;
using SpeechRecognition.CrossCutting.DTO;
using SpeechRecognition.CrossCutting.Framework.Interfaces;
using SpeechRecognition.Infra.Context;
using SpeechRecognition.Infra.FireStore.Context;
using SpeechRecognition.Infra.FireStore.Repositories.Aggregates;
using SpeechRecognition.Infra.Repositories.Aggregates;
using SpeechRecognition.Infra.UoW;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.CrossCutting.IoC.DataBase
{
    public static class FireStoreIoC
    {
        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
        {

            #region Unit of Work

            services.AddScoped<IUnitOfWork, FireStoreUnitOfWork>();

            #endregion

            #region Context    

            services.AddSingleton(provider =>
            {
                var fireStoreConfig = ConfigurationDTO.GetFireStoreConfig(configuration);
                var config = new FirestoreDbContext(fireStoreConfig.ProjectId, fireStoreConfig.CredentialsPath);
                return config.Db;
            });

            #endregion

            #region Repositories

            services.AddScoped<IFileStorageAggregateRepository, FireStoreFileStorageAggregateRepository>();

            #endregion

            return services;
        }
    }
}
