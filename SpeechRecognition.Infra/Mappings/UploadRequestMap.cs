using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpeechRecognition.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.Mappings
{
    internal class UploadRequestMap : IEntityTypeConfiguration<UploadRequest>
    {
        public void Configure(EntityTypeBuilder<UploadRequest> builder)
        {
            builder.ToTable("UploadRequest");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FileStorageId)
                .IsRequired();

            builder.HasOne(x => x.FileStorage )
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
