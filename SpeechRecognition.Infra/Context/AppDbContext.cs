using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.Context
{
    using Microsoft.EntityFrameworkCore;
    using SpeechRecognition.Dominio.Entidades;
    using SpeechRecognition.Infra.Mappings;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<FileStorage> FileStorages { get; set; }
        public DbSet<FileStorageConversion> FileStorageConversions { get; set; }
        public DbSet<AudioTranslation> AudioTranslations { get; set; }
        public DbSet<RabbitMqLog> RabbitMqLogs { get; set; }
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
