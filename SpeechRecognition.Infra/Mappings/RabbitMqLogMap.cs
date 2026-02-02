using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpeechRecognition.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.Mappings
{
    public class RabbitMqLogMap : IEntityTypeConfiguration<RabbitMqLog>
    {
        public void Configure(EntityTypeBuilder<RabbitMqLog> builder)
        {
            builder.ToTable("RabbitMqLog");

            builder.HasKey(x => x.Id);
        }
    }
}
