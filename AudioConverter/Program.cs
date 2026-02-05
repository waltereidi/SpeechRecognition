using AudioConverter.Handlers;
using BuildingBlocks.Messaging;
using BuildingBlocks.Messaging.MassTransit;
using MassTransit;
using Shared.Events.AudioConverter;

var builder = WebApplication.CreateBuilder(args);


// Registra o handler de eventos
builder.Services.AddIntegrationEventHandler<AudioConversionToWav16kLocalEvent, AudioConversionToWav16kLocalHandler>();

// Configuração do MassTransit com RabbitMQ para consumir mensagens
builder.Services.AddMassTransit(busConfigurator =>
{
    // Registra o consumidor genérico para o evento PedidoCriado
    busConfigurator.AddConsumer<GenericConsumer<AudioConversionToWav16kLocalEvent>>();
    busConfigurator.AddMessaging(config =>
    {
        config.Host = "rabbitmq";
        config.Username = "admin";
        config.Password = "admin";
        config.Port = 5672;
        config.EnableLogging = true;
    });
    
    busConfigurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", 5672, "/", hostCfg =>
        {
            hostCfg.Username("admin");
            hostCfg.Password("admin");
        });

        // Configura o endpoint para receber os eventos de pedido
        cfg.ReceiveEndpoint("audio-translation-queue", endpointCfg =>
        {
            endpointCfg.ConfigureConsumer<GenericConsumer<AudioConversionToWav16kLocalEvent>>(context);
        });
    });
});

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