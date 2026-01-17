using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpeechRecognition.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.Mappings
{
    public class TranslationTemplateMap : IEntityTypeConfiguration<TranslationTemplate>
    {
        public void Configure(EntityTypeBuilder<TranslationTemplate> builder)
        {
            builder.ToTable("TranslationTemplate");

            builder.HasKey(x => x.Id);

        }
    }
}
