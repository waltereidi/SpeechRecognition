using BuildingBlocks.Messaging.Abstractions;

namespace BuildingBlocks.Messaging.Events;

/// <summary>
/// Classe base abstrata para eventos de integração.
/// Todas as implementações de eventos de integração devem herdar desta classe.
/// </summary>
public abstract record IntegrationEvent : IIntegrationEvent
{
    /// <summary>
    /// Identificador único do evento, gerado automaticamente.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Data e hora em que o evento ocorreu (UTC).
    /// </summary>
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Tipo do evento, derivado do nome da classe.
    /// </summary>
    public string EventType => GetType().Name;
}
