using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpeechRecognition.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.Mappings
{
    public class AudioTranslationMap : IEntityTypeConfiguration<AudioTranslation>
    {
        public void Configure(EntityTypeBuilder<AudioTranslation> builder)
        {
            builder.ToTable("AudioTranslation");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Translation)
                .IsRequired()
                .HasColumnType("text"); // ou nvarchar(max) / varchar(max)

            builder.Property(x => x.FileStorageId)
                .IsRequired();

            builder.HasOne(x => x.FileStorage)
                .WithMany() // FileStorage não tem coleção
                .HasForeignKey(x => x.FileStorageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.TranslationTemplate)
                .WithMany() // FileStorage não tem coleção
                .HasForeignKey(x => x.TranslationTemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
