namespace SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;

/// <summary>
/// Interface para publicação de eventos de integração.
/// Permite que serviços publiquem eventos sem depender diretamente da implementação do barramento.
/// </summary>
public interface IIntegrationEventPublisher
{
    /// <summary>
    /// Publica um evento de integração.
    /// </summary>
    /// <typeparam name="TEvent">Tipo do evento a ser publicado.</typeparam>
    /// <param name="event">O evento a ser publicado.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Task representando a operação assíncrona.</returns>
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, IIntegrationEvent;
}
