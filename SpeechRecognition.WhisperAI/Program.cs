using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.MassTransit;
using MassTransit;
using SpeechRecognition.CrossCutting.Shared.Events.WhisperSpeechRecognition;
using SpeechRecognition.WhisperAI.DTO;
using SpeechRecognition.WhisperAI.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Registra o handler de eventos
builder.Services.AddIntegrationEventHandler<AudioTranslationEvent, AudioTranslationHandler>();
var configuration = new ConfigurationDTO();
//builder.Configuration.AddConfiguration(configuration.GetCofiguration());


// Configuração do MassTransit com RabbitMQ para consumir mensagens
builder.Services.AddMassTransit(busConfigurator =>
{
    // Registra o consumidor genérico para o evento PedidoCriado
    busConfigurator.AddConsumer<GenericConsumer<AudioTranslationEvent>>();

    busConfigurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(configuration.RabbitMqConfig.HostName, configuration.RabbitMqConfig.Port, "/", hostCfg =>
        {
            hostCfg.Username(configuration.RabbitMqConfig.UserName);
            hostCfg.Password(configuration.RabbitMqConfig.Password);
        });
        // Configura retry para tratamento de falhas
        cfg.UseMessageRetry(retryCfg =>
        {
            retryCfg.Interval(3, TimeSpan.FromSeconds(5));
        });

        // Configura o endpoint para receber os eventos de pedido
        cfg.ReceiveEndpoint("audio-translation-queue", endpointCfg =>
        {
            endpointCfg.ConfigureConsumer<GenericConsumer<AudioTranslationEvent>>(context);
        });
    });
});

var app = builder.Build();
// Endpoint de health check
app.MapGet("/health", () => Results.Ok(new { Status = "Saudável", Servico = "WhisperSpeechRecognition.Api" }));

// Endpoint para verificar status das filas
app.MapGet("/status", () => Results.Ok(new
{
    Status = "Consumidor ativo",
    Fila = "audio-translation-queue",
    Mensagem = "Aguardando mensagens..."
}));

app.Run();
