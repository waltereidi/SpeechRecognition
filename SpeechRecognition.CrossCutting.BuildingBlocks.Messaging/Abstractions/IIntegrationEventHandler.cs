namespace SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;

/// <summary>
/// Interface para handlers que processam eventos de integração.
/// Cada handler é responsável por processar um tipo específico de evento.
/// </summary>
/// <typeparam name="TEvent">Tipo do evento de integração a ser processado.</typeparam>
public interface IIntegrationEventHandler<in TEvent> where TEvent : IIntegrationEvent
{
    /// <summary>
    /// Processa o evento de integração recebido.
    /// </summary>
    /// <param name="event">O evento a ser processado.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Task representando a operação assíncrona.</returns>
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}
