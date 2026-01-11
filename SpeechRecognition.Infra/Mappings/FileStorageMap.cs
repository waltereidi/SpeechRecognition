using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpeechRecognition.Dominio.Entidades;
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
