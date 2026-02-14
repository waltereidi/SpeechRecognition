using System.Diagnostics;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.MassTransit.Filters;

/// <summary>
/// Filtro de logging para consumo de mensagens.
/// Registra informações detalhadas sobre cada mensagem processada, incluindo tempo de execução.
/// </summary>
/// <typeparam name="T">Tipo da mensagem sendo consumida.</typeparam>
public class LoggingFilter<T> : IFilter<ConsumeContext<T>> where T : class
{
    private readonly ILogger<LoggingFilter<T>> _logger;

    /// <summary>
    /// Inicializa uma nova instância do filtro de logging.
    /// </summary>
    /// <param name="logger">Logger para registro de operações.</param>
    public LoggingFilter(ILogger<LoggingFilter<T>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Executa o filtro de logging antes e depois do processamento da mensagem.
    /// </summary>
    /// <param name="context">Contexto de consumo da mensagem.</param>
    /// <param name="next">Próximo filtro ou consumidor na pipeline.</param>
    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        var messageType = typeof(T).Name;
        var messageId = context.MessageId;
        var correlationId = context.CorrelationId;
        var conversationId = context.ConversationId;

        _logger.LogInformation(
            "Iniciando processamento de mensagem. Tipo: {TipoMensagem}, MessageId: {MessageId}, CorrelationId: {CorrelationId}, ConversationId: {ConversationId}",
            messageType,
            messageId,
            correlationId,
            conversationId);

        var stopwatch = Stopwatch.StartNew();

        try
        {
            await next.Send(context);

            stopwatch.Stop();

            _logger.LogInformation(
                "Mensagem processada com sucesso. Tipo: {TipoMensagem}, MessageId: {MessageId}, Duração: {DuracaoMs}ms",
                messageType,
                messageId,
                stopwatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            _logger.LogError(
                ex,
                "Erro durante processamento de mensagem. Tipo: {TipoMensagem}, MessageId: {MessageId}, Duração: {DuracaoMs}ms",
                messageType,
                messageId,
                stopwatch.ElapsedMilliseconds);

            throw;
        }
    }

    /// <summary>
    /// Sonda o contexto para verificar se o filtro deve ser aplicado.
    /// </summary>
    /// <param name="context">Contexto de sondagem.</param>
    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope("logging-filter");
    }
}


