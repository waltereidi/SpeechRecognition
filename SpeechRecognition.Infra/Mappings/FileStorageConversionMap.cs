using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.FileStorageDomain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.Mappings
{
    public class FileStorageConversionMap
    {
        public void Configure(OwnedNavigationBuilder<FileStorageAggregate, FileStorageConversion> builder)
        {
            builder.ToTable("FileStorageConversion");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FileStorageId)
                .IsRequired();

            builder.Property(x => x.FileStorageAggregateId)
                .IsRequired();
            
            var fileStorageConversionId = new ValueConverter<FileStorageConversionId, string>(
                v => ((Guid)v).ToString(),           // Guid -> string
                v => new FileStorageConversionId(Guid.Parse(v)));

            builder.Property(x => x.Id)
                .HasConversion(fileStorageConversionId)
                .HasMaxLength(36)
                .IsRequired();


            var fileStorageId = new ValueConverter<FileStorageId, string>(
                v => ((Guid)v).ToString(),           // Guid -> string
                v => new FileStorageId(Guid.Parse(v)));

            builder.Property(x => x.FileStorageId)
                .HasConversion(fileStorageId)
                .HasMaxLength(36)
                .IsRequired();

            var guidToStringConverter = new ValueConverter<FileStorageAggregateId, string>(
                v => ((Guid)v).ToString(),           // Guid -> string
                v => new FileStorageAggregateId(Guid.Parse(v)));

            builder.Property(x => x.FileStorageAggregateId)
                .HasConversion(guidToStringConverter)
                .HasMaxLength(36)
                .IsRequired();

        }

    }
}
