using AudioConverter.Handlers;
using BuildingBlocks.Messaging;
using BuildingBlocks.Messaging.Abstractions;
using BuildingBlocks.Messaging.MassTransit;
using MassTransit;
using Shared.Events.AudioConverter;

var builder = WebApplication.CreateBuilder(args);


// Registra o handler de eventos
builder.Services.AddIntegrationEventHandler<AudioConversionToWav16kLocalEvent, AudioConversionToWav16kLocalHandler>();

// Mensagens e consumidores
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<GenericConsumer<AudioConversionToWav16kLocalEvent>>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", 5672, "/", hostCfg =>
        {
            hostCfg.Username("admin");
            hostCfg.Password("admin");
        });
        // Fila única para consumo
        cfg.ReceiveEndpoint("audio-translation-queue", endpointCfg =>
        {
            endpointCfg.ConfigureConsumer<GenericConsumer<AudioConversionToWav16kLocalEvent>>(context);
        });
    });
});

// Registra o adaptador que expõe IEventBus usando a infra do MassTransit
builder.Services.AddScoped<IEventBus, MassTransitEventBus>();

var app = builder.Build();

// ===============================
// Health Check
// ===============================
app.MapGet("/health", () =>
    Results.Ok(new
    {
        Status = "Saudável",
        Servico = "AudioConverter.Api"
    })
);
// ===============================
// Status do consumidor
// ===============================
app.MapGet("/status", () =>
    Results.Ok(new
    {
        Status = "Consumidor ativo",
        Fila = "audio-converter-queue",
        Mensagem = "Aguardando mensagens..."
    })
);

app.Run();