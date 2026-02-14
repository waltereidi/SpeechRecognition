using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.MassTransit;

/// <summary>
/// Implementação do barramento de eventos utilizando MassTransit.
/// Fornece funcionalidades de publicação de eventos através do RabbitMQ.
/// </summary>
public class MassTransitEventBus : IEventBus, IIntegrationEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<MassTransitEventBus> _logger;

    /// <summary>
    /// Inicializa uma nova instância do barramento de eventos MassTransit.
    /// </summary>
    /// <param name="publishEndpoint">Endpoint de publicação do MassTransit.</param>
    /// <param name="logger">Logger para registro de operações.</param>
    public MassTransitEventBus(
        IPublishEndpoint publishEndpoint,
        ILogger<MassTransitEventBus> logger)
    {
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, IIntegrationEvent
    {
        ArgumentNullException.ThrowIfNull(@event);

        _logger.LogInformation(
            "Publicando evento de integração. Tipo: {TipoEvento}, Id: {EventoId}, OcorridoEm: {OcorridoEm}",
            @event.EventType,
            @event.Id,
            @event.OccurredOn);

        try
        {
            await _publishEndpoint.Publish(@event, cancellationToken);

            _logger.LogDebug(
                "Evento publicado com sucesso. Tipo: {TipoEvento}, Id: {EventoId}",
                @event.EventType,
                @event.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Erro ao publicar evento. Tipo: {TipoEvento}, Id: {EventoId}",
                @event.EventType,
                @event.Id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task PublishAsync<TEvent>(IEnumerable<TEvent> events, CancellationToken cancellationToken = default)
        where TEvent : class, IIntegrationEvent
    {
        ArgumentNullException.ThrowIfNull(events);

        var eventList = events.ToList();
        
        _logger.LogInformation(
            "Publicando lote de {Quantidade} eventos de integração",
            eventList.Count);

        foreach (var @event in eventList)
        {
            await PublishAsync(@event, cancellationToken);
        }

        _logger.LogDebug(
            "Lote de {Quantidade} eventos publicado com sucesso",
            eventList.Count);
    }
}
