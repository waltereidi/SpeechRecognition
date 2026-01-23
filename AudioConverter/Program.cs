using AudioConverter.Handlers;
using BuildingBlocks.Messaging;
using BuildingBlocks.Messaging.MassTransit;
using MassTransit;
using Shared.Events;
using AudioConverter.Handlers;


var builder = WebApplication.CreateBuilder(args);

// Registra o handler de eventos
builder.Services.AddIntegrationEventHandler<AudioConversionEvent, AudioConversionHandler>();

// Configuração do MassTransit com RabbitMQ para consumir mensagens
builder.Services.AddMassTransit(busConfigurator =>
{
    // Registra o consumidor genérico para o evento PedidoCriado
    busConfigurator.AddConsumer<GenericConsumer<AudioConversionEvent>>();

    busConfigurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", 5675, "/", hostCfg =>
        {
            hostCfg.Username("guest");
            hostCfg.Password("guest");
        });

        // Configura retry para tratamento de falhas
        cfg.UseMessageRetry(retryCfg =>
        {
            retryCfg.Interval(3, TimeSpan.FromSeconds(5));
        });

        // Configura o endpoint para receber os eventos de pedido
        cfg.ReceiveEndpoint("audio-converter-queue", endpointCfg =>
        {
            endpointCfg.ConfigureConsumer<GenericConsumer<AudioConversionEvent>>(context);
        });
    });
});

var app = builder.Build();
// Endpoint de health check
app.MapGet("/health", () => Results.Ok(new { Status = "Saudável", Servico = "AudioConverter.Api" }));

// Endpoint para verificar status das filas
app.MapGet("/status", () => Results.Ok(new
{
    Status = "Consumidor ativo",
    Fila = "audio-converter-queue",
    Mensagem = "Aguardando mensagens..."
}));

app.Run();