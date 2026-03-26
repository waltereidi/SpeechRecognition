using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpeechRecognition.Application.Interfaces;
using SpeechRecognition.CrossCutting.Framework.Interfaces;
using SpeechRecognition.Infra.Context;
using SpeechRecognition.Infra.Repositories.Aggregates;
using SpeechRecognition.Infra.UoW;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.CrossCutting.IoC.DataBase
{
    public static class PostgreSQLIoC
    {
        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
        {
           
            #region Unit of Work

            services.AddScoped<IUnitOfWork, PostgresqlUnitOfWork>();

            #endregion

            #region Context    

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetSection("ConnectionStrings:Default").Value);
            });

            #endregion

            #region Repositories
            services.AddScoped<IFileStorageAggregateRepository, FileStorageAggregateRepository>();
            #endregion

            return services;
        }
    }
}
