using BuildingBlocks.Messaging;
using BuildingBlocks.Messaging.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using SpeechRecognition.Infra.Context;


var builder = WebApplication.CreateBuilder(args);

// Configuração do MassTransit com RabbitMQ
builder.Services.AddMessaging(config =>
{
    config.Host = "localhost";
    config.Username = "admin";
    config.Password = "admin";
    config.Port = 5672;
    config.EnableLogging = true;
});

var app = builder.Build();
// Endpoint para criar e publicar um pedido
app.MapPost("/api/pedidos", async (IEventBus eventBus) =>
{

    // Cria o evento de pedido criado   
    var pedidoCriadoEvent = new PedidoCriadoEvent();

    // Publica o evento no barramento
    await eventBus.PublishAsync(pedidoCriadoEvent);

    return Results.Created($"/api/pedidos/{pedidoCriadoEvent.PedidoId}", new
    {
        pedidoCriadoEvent.PedidoId,
        Mensagem = "Pedido criado e publicado na fila com sucesso!"
    });
});

// Endpoint de health check
app.MapGet("/health", () => Results.Ok(new { Status = "Saudável", Servico = "Producer.Api" }));

app.Run();
