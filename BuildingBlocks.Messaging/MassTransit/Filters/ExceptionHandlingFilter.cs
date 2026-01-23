using MassTransit;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Messaging.MassTransit.Filters;

/// <summary>
/// Filtro para tratamento centralizado de exceções durante o consumo de mensagens.
/// Captura exceções, registra detalhes e permite tratamento customizado.
/// </summary>
/// <typeparam name="T">Tipo da mensagem sendo consumida.</typeparam>
public class ExceptionHandlingFilter<T> : IFilter<ConsumeContext<T>> where T : class
{
    private readonly ILogger<ExceptionHandlingFilter<T>> _logger;

    /// <summary>
    /// Inicializa uma nova instância do filtro de tratamento de exceções.
    /// </summary>
    /// <param name="logger">Logger para registro de erros.</param>
    public ExceptionHandlingFilter(ILogger<ExceptionHandlingFilter<T>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Executa o filtro, capturando e tratando exceções que ocorram no processamento.
    /// </summary>
    /// <param name="context">Contexto de consumo da mensagem.</param>
    /// <param name="next">Próximo filtro ou consumidor na pipeline.</param>
    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        var messageType = typeof(T).Name;
        var messageId = context.MessageId;

        try
        {
            await next.Send(context);
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogWarning(
                ex,
                "Operação cancelada durante processamento de mensagem. Tipo: {TipoMensagem}, MessageId: {MessageId}",
                messageType,
                messageId);

            // Não re-lança para operações canceladas, permite que o MassTransit trate adequadamente
            throw;
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(
                ex,
                "Erro de validação de argumento na mensagem. Tipo: {TipoMensagem}, MessageId: {MessageId}, Parâmetro: {NomeParametro}",
                messageType,
                messageId,
                ex.ParamName);

            // Re-lança para que o MassTransit possa enviar para a fila de erro
            throw;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(
                ex,
                "Operação inválida durante processamento. Tipo: {TipoMensagem}, MessageId: {MessageId}",
                messageType,
                messageId);

            throw;
        }
        catch (TimeoutException ex)
        {
            _logger.LogError(
                ex,
                "Timeout durante processamento de mensagem. Tipo: {TipoMensagem}, MessageId: {MessageId}",
                messageType,
                messageId);

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(
                ex,
                "Erro não tratado durante processamento de mensagem. Tipo: {TipoMensagem}, MessageId: {MessageId}, Exceção: {TipoExcecao}",
                messageType,
                messageId,
                ex.GetType().Name);

            // Registra informações adicionais sobre a mensagem para diagnóstico
            LogMessageDetails(context);

            throw;
        }
    }

    /// <summary>
    /// Registra detalhes adicionais da mensagem para diagnóstico.
    /// </summary>
    /// <param name="context">Contexto de consumo da mensagem.</param>
    private void LogMessageDetails(ConsumeContext<T> context)
    {
        try
        {
            _logger.LogDebug(
                "Detalhes da mensagem com erro. CorrelationId: {CorrelationId}, ConversationId: {ConversationId}, SourceAddress: {SourceAddress}, DestinationAddress: {DestinationAddress}",
                context.CorrelationId,
                context.ConversationId,
                context.SourceAddress,
                context.DestinationAddress);

            // Registra headers se existirem
            if (context.Headers.Any())
            {
                var headers = string.Join(", ", context.Headers.Select(h => $"{h.Key}={h.Value}"));
                _logger.LogDebug("Headers da mensagem: {Headers}", headers);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(
                ex,
                "Não foi possível registrar detalhes adicionais da mensagem");
        }
    }

    /// <summary>
    /// Sonda o contexto para verificar se o filtro deve ser aplicado.
    /// </summary>
    /// <param name="context">Contexto de sondagem.</param>
    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope("exception-handling-filter");
    }
}


