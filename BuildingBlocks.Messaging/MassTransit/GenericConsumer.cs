using BuildingBlocks.Messaging.Abstractions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Messaging.MassTransit;

/// <summary>
/// Consumidor genérico que conecta eventos do MassTransit aos handlers de eventos de integração.
/// Atua como adaptador entre o MassTransit e a aplicação.
/// </summary>
/// <typeparam name="TEvent">Tipo do evento de integração a ser consumido.</typeparam>
public class GenericConsumer<TEvent> : IConsumer<TEvent>
    where TEvent : class, IIntegrationEvent
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<GenericConsumer<TEvent>> _logger;

    /// <summary>
    /// Inicializa uma nova instância do consumidor genérico.
    /// </summary>
    /// <param name="serviceProvider">Provedor de serviços para resolução de handlers.</param>
    /// <param name="logger">Logger para registro de operações.</param>
    public GenericConsumer(
        IServiceProvider serviceProvider,
        ILogger<GenericConsumer<TEvent>> logger)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Consome uma mensagem do MassTransit e delega para o handler apropriado.
    /// </summary>
    /// <param name="context">Contexto de consumo do MassTransit.</param>
    public async Task Consume(ConsumeContext<TEvent> context)
    {
        var @event = context.Message;
        var eventTypeName = typeof(TEvent).Name;

        _logger.LogInformation(
            "Recebendo evento de integração. Tipo: {TipoEvento}, Id: {EventoId}, MessageId: {MessageId}",
            eventTypeName,
            @event.Id,
            context.MessageId);

        try
        {
            // Resolve todos os handlers registrados para este tipo de evento
            var handlers = _serviceProvider.GetServices<IIntegrationEventHandler<TEvent>>();
            var handlerList = handlers.ToList();

            if (!handlerList.Any())
            {
                _logger.LogWarning(
                    "Nenhum handler encontrado para o evento. Tipo: {TipoEvento}, Id: {EventoId}",
                    eventTypeName,
                    @event.Id);
                return;
            }

            _logger.LogDebug(
                "Encontrados {QuantidadeHandlers} handler(s) para o evento. Tipo: {TipoEvento}",
                handlerList.Count,
                eventTypeName);

            // Executa todos os handlers encontrados
            foreach (var handler in handlerList)
            {
                var handlerTypeName = handler.GetType().Name;

                _logger.LogDebug(
                    "Executando handler {NomeHandler} para evento {TipoEvento}",
                    handlerTypeName,
                    eventTypeName);

                await handler.HandleAsync(@event, context.CancellationToken);

                _logger.LogDebug(
                    "Handler {NomeHandler} executado com sucesso para evento {TipoEvento}",
                    handlerTypeName,
                    eventTypeName);
            }

            _logger.LogInformation(
                "Evento processado com sucesso. Tipo: {TipoEvento}, Id: {EventoId}",
                eventTypeName,
                @event.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Erro ao processar evento. Tipo: {TipoEvento}, Id: {EventoId}",
                eventTypeName,
                @event.Id);
            throw;
        }
    }
}
