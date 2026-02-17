using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.FileStorageDomain.Enum;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace SpeechRecognition.Infra.Mappings
{
    public class RabbitMqLogMap : IEntityTypeConfiguration<RabbitMqLog>
    {
        public void Configure(EntityTypeBuilder<RabbitMqLog> builder)
        {
            builder.ToTable("RabbitMqLog");

            builder.HasKey(x => x.Id);

            var statusConverter = new EnumToStringConverter<LogSeverity>();
            
            builder.Property(p => p.Severity)
                .HasConversion(statusConverter)
                .HasConversion<string>()
                .HasMaxLength(20);

            var guidToStringConverter = new ValueConverter<Guid, string>(
                v => v.ToString("D"),           // Guid -> string
                v => Guid.Parse(v)              // string -> Guid
            );

            var fileStorageAggregateId = new ValueConverter<FileStorageAggregateId, string>(
                v => ((Guid)v).ToString(),           // Guid -> string
                v => new FileStorageAggregateId(Guid.Parse(v)));

            builder.Property(x => x.FileStorageAggregateId)
                .HasConversion(fileStorageAggregateId)
                .HasMaxLength(36)
                .IsRequired();

        }
    }
}
