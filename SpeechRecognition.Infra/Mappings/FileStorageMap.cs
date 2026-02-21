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
    public static class FileStorageMap
    {
        public static void Configure(OwnedNavigationBuilder<FileStorageAggregate, FileStorage> builder)
        {
            builder.ToTable("FileStorage");

            builder.HasKey(x => x.Id);

            // Converter FileInfo <-> string (FullName)
            var fileStorageConversionId = new ValueConverter<FileStorageId, string>(
                v => ((Guid)v).ToString(),           // Guid -> string
                v => new FileStorageId(Guid.Parse(v)));

            builder.Property(x => x.Id)
                .HasConversion(fileStorageConversionId)
                .HasMaxLength(36)
                .IsRequired();

            var fileInfoConverter = new ValueConverter<FileInfo, string>(
                v => v.FullName,                // Para o banco
                v => new FileInfo(v)            // Para o domínio
            );

            builder.Property(x => x.FileInfo)
                .HasConversion(fileInfoConverter)
                .HasColumnName("FileFullName")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.OriginalFileName)
                .HasMaxLength(255)
                .IsRequired();


        }


    }
}
