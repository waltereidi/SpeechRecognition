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
    public class FileStorageMap : IEntityTypeConfiguration<FileStorage>
    {
        public void Configure(EntityTypeBuilder<FileStorage> builder)
        {
            builder.ToTable("FileStorage");

            builder.HasKey(x => x.Id);

            // Converter FileInfo <-> string (FullName)
            var fileInfoConverter = new ValueConverter<FileInfo, string>(
                v => v.FullName,                // Para o banco
                v => new FileInfo(v)            // Para o domínio
            );
            var guidToStringConverter = new ValueConverter<FileStorageAggregateId, string>(
                v => ((Guid)v).ToString(),           // Guid -> string
                v => new FileStorageAggregateId(Guid.Parse(v))              // string -> Guid
            );

            builder.Property(x => x.FileInfo)
                .HasConversion(fileInfoConverter)
                .HasColumnName("FileFullName")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.OriginalFileName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.FileStorageAggregateId)
                .HasConversion(guidToStringConverter)
                .HasMaxLength(36)
                .IsRequired();

        }
    }
}
