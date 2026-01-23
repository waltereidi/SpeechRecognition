namespace BuildingBlocks.Messaging.Abstractions;

/// <summary>
/// Interface que define um evento de integração entre serviços.
/// Eventos de integração são usados para comunicação assíncrona entre microserviços.
/// </summary>
public interface IIntegrationEvent
{
    /// <summary>
    /// Identificador único do evento.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Data e hora em que o evento foi criado (UTC).
    /// </summary>
    DateTime OccurredOn { get; }

    /// <summary>
    /// Tipo do evento para identificação durante o consumo.
    /// </summary>
    string EventType { get; }
}
