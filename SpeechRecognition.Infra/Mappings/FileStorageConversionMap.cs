using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpeechRecognition.Dominio.Entidades;
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
        }

    }
}
