namespace SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;

/// <summary>
/// Interface do barramento de eventos para publicação e assinatura de eventos de integração.
/// Fornece uma abstração sobre a infraestrutura de mensageria utilizada.
/// </summary>
public interface IEventBus
{
    /// <summary>
    /// Publica um evento de integração no barramento.
    /// </summary>
    /// <typeparam name="TEvent">Tipo do evento a ser publicado.</typeparam>
    /// <param name="event">O evento a ser publicado.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Task representando a operação assíncrona.</returns>
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, IIntegrationEvent;

    /// <summary>
    /// Publica múltiplos eventos de integração no barramento.
    /// </summary>
    /// <typeparam name="TEvent">Tipo dos eventos a serem publicados.</typeparam>
    /// <param name="events">Os eventos a serem publicados.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Task representando a operação assíncrona.</returns>
    Task PublishAsync<TEvent>(IEnumerable<TEvent> events, CancellationToken cancellationToken = default)
        where TEvent : class, IIntegrationEvent;
}
