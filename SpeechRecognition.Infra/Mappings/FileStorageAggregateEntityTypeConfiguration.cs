using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            builder.OwnsMany(x => x.FileStorages);
            builder.OwnsMany(x => x.Logs);
            builder.OwnsMany(x => x.AudioTranslations);
            builder.OwnsMany(x => x.FileStorageConversions);


        }
    }
}
