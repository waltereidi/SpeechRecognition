using AudioConverter.Handlers;
using AudioConverter.Interfaces;
using AudioConverter.Services.Linux;
using BuildingBlocks.Messaging;
using BuildingBlocks.Messaging.MassTransit;
using MassTransit;
using Shared.Events;

var builder = WebApplication.CreateBuilder(args);


// Registra o handler de eventos
builder.Services.AddIntegrationEventHandler<AudioConversionToWav16kEvent, AudioConversionToWav16kHandler>();


// Configuração do MassTransit com RabbitMQ para consumir mensagens
builder.Services.AddMassTransit(busConfigurator =>
{
    // Registra o consumidor genérico para o evento PedidoCriado
    busConfigurator.AddConsumer<GenericConsumer<AudioConversionToWav16kEvent>>();

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
            endpointCfg.ConfigureConsumer<GenericConsumer<AudioConversionToWav16kEvent>>(context);
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
// Teste de conversão FFmpeg (Linux/macOS)
// ===============================
app.MapGet("/testAudioConversionLinux",
    async () =>
    {
        var di = new DirectoryInfo("Files");
        var input = "Files/output.wav";
        var output = "Files/Output/TestAudioConverter.wav";

        IAudioConversionStrategy strategy = new ConvertToWavMono16kLinux(
            new FileInfo(input),
            di
        );
        await strategy.Start(CancellationToken.None);

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