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
    public class RabbitMqLogMap 
    {
        public void Configure(OwnedNavigationBuilder<FileStorageAggregate, RabbitMqLog> builder)
        {
            builder.ToTable("RabbitMqLog");

            builder.HasKey(x => x.Id);

            var statusConverter = new EnumToStringConverter<LogSeverity>();
            
            builder.Property(p => p.Severity)
                .HasConversion(statusConverter)
                .HasConversion<string>()
                .HasMaxLength(20);

            var rabbitMqLogId = new ValueConverter<RabbitMqLogId, string>(
                v => ((Guid)v).ToString(),           // Guid -> string
                v => new RabbitMqLogId(Guid.Parse(v)));

            builder.Property(x => x.Id)
                .HasConversion(rabbitMqLogId)
                .HasMaxLength(36)
                .IsRequired();


            //var fileStorageAggregateId = new ValueConverter<FileStorageAggregateId, string>(
            //    v => ((Guid)v).ToString(),           // Guid -> string
            //    v => new FileStorageAggregateId(Guid.Parse(v)));

            //builder.Property(x => x.FileStorageAggregateId)
            //    .HasConversion(fileStorageAggregateId)
            //    .HasMaxLength(36)
            //    .IsRequired();

        }
    }
}
