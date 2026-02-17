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
    public class AudioTranslationMap
    {
        public void Configure(OwnedNavigationBuilder<FileStorageAggregate ,AudioTranslation> builder)
        {
            builder.ToTable("AudioTranslation");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Translation)
                .IsRequired()
                .HasColumnType("text"); // ou nvarchar(max) / varchar(max)

            builder.Property(x => x.FileStorageConversionId)
                .IsRequired();

            var audioTranslationId = new ValueConverter<AudioTranslationId , string>(
                v => ((Guid)v).ToString(),           // Guid -> string
                v => new AudioTranslationId(Guid.Parse(v)));


            builder.Property(x => x.Id)
                .HasConversion(audioTranslationId)
                .HasMaxLength(36)
                .IsRequired();

            var fileStorageConverterId = new ValueConverter<FileStorageConversionId, string>(
                v => ((Guid)v).ToString(),           // Guid -> string
                v => new FileStorageConversionId(Guid.Parse(v)));


            builder.Property(x => x.FileStorageConversionId)
                .HasConversion(fileStorageConverterId)
                .HasMaxLength(36)
                .IsRequired();

            var guidToStringConverter = new ValueConverter<FileStorageAggregateId, string>(
                v => ((Guid)v).ToString(),           // Guid -> string
                v => new FileStorageAggregateId(Guid.Parse(v)));

            builder.Property(x => x.FileStorageAggregateId)
                .HasConversion(guidToStringConverter)
                .HasMaxLength(36)
                .IsRequired();

            builder.Property(p => p.WhisperModel)
               .HasConversion<int>()
               .IsRequired();
        }
    }
}
