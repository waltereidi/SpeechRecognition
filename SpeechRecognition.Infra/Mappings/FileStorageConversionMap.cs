using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpeechRecognition.FileStorageDomain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.Mappings
{
    public class FileStorageConversionMap : IEntityTypeConfiguration<FileStorageConversion>
    {
        public void Configure(EntityTypeBuilder<FileStorageConversion> builder)
        {
            builder.ToTable("FileStorageConversion");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FileStorageId)
                .IsRequired();

            builder.HasOne(x => x.FileStorage)
                .WithMany() // sem coleção no FileStorage
                .HasForeignKey(x => x.FileStorageId)
                .OnDelete(DeleteBehavior.Cascade);

            var guidToStringConverter = new ValueConverter<Guid, string>(
                v => v.ToString("D"),           // Guid -> string
                v => Guid.Parse(v)              // string -> Guid
            );

            builder.Property(x => x.FileStorageId)
                .HasConversion(guidToStringConverter)
                .HasMaxLength(36)
                .IsRequired();
        }

    }
}
