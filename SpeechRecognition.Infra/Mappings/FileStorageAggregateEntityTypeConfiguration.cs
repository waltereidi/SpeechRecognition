using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpeechRecognition.FileStorageDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.Mappings
{
    public class FileStorageAggregateEntityTypeConfiguration : IEntityTypeConfiguration<FileStorageAggregate>
    {
        public void Configure(EntityTypeBuilder<FileStorageAggregate> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.OwnsMany(x => x.FileStorages , fs => FileStorageMap.Configure(fs) );
            builder.OwnsMany(x => x.Logs , l => new RabbitMqLogMap().Configure(l)
            );
            builder.OwnsMany(x => x.AudioTranslations , at => new AudioTranslationMap().Configure(at) );
            builder.OwnsMany(x => x.FileStorageConversions , fsc => new FileStorageConversionMap().Configure(fsc) );

            var fileStorageAggregateId = new ValueConverter<FileStorageAggregateId, string>(
                v => ((Guid)v).ToString(),           // Guid -> string
                v => new FileStorageAggregateId(Guid.Parse(v)));

            builder.Property(x => x.Id)
                .HasConversion(fileStorageAggregateId)
                .HasMaxLength(36)
                .IsRequired();

        }
    }
}
