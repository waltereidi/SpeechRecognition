using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.Context
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using SpeechRecognition.Infra.Mappings;

    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.AddInterceptors(new AuditInterceptor());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FileStorageMap());
            modelBuilder.ApplyConfiguration(new FileStorageConversionMap());
            modelBuilder.ApplyConfiguration(new AudioTranslationMap());
        }
    }
}
