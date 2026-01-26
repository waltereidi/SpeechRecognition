using AudioConverter.Handlers;
using AudioConverter.Services.Linux;
using BuildingBlocks.Messaging;
using BuildingBlocks.Messaging.MassTransit;
using MassTransit;
using Shared.Events;

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
        cfg.ReceiveEndpoint("audio-translation-queue", endpointCfg =>
        {
            endpointCfg.ConfigureConsumer<GenericConsumer<AudioConversionEvent>>(context);
        });
    });
});
// ===============================
// Dependency Injection
// ===============================
builder.Services.AddSingleton<FfmpegExecutor>();
builder.Services.AddScoped<AudioService>();

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
// Teste de conversão FFmpeg (Linux/macOS)
// ===============================
app.MapGet("/testAudioConversionLinux",
    async (AudioService audioService) =>
    {
        var di = new DirectoryInfo("Files");
        var input = "Files/output.wav";
        var output = "Files/Output/TestAudioConverter.wav";

        await audioService.ExtractWavAsync(
            input,
            output
        );

        return Results.Ok(new
        {
            Status = "Conversão realizada",
            Entrada = input,
            Saida = output
        });
    }
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