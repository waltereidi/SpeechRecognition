namespace SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.MassTransit;

/// <summary>
/// Configurações para conexão com o RabbitMQ via MassTransit.
/// </summary>
public class MassTransitConfiguration
{
    /// <summary>
    /// Nome da seção de configuração no appsettings.
    /// </summary>
    public const string SectionName = "MassTransit";

    /// <summary>
    /// Host do RabbitMQ. Padrão: localhost.
    /// </summary>
    public string Host { get; set; } = "localhost";

    /// <summary>
    /// Nome de usuário para autenticação no RabbitMQ. Padrão: guest.
    /// </summary>
    public string Username { get; set; } = "guest";

    /// <summary>
    /// Senha para autenticação no RabbitMQ. Padrão: guest.
    /// </summary>
    public string Password { get; set; } = "guest";

    /// <summary>
    /// Virtual host do RabbitMQ. Padrão: /.
    /// </summary>
    public string VirtualHost { get; set; } = "/";

    /// <summary>
    /// Porta do RabbitMQ. Padrão: 5672.
    /// </summary>
    public int Port { get; set; } = 5672;

    /// <summary>
    /// Número de tentativas de retry antes de enviar para a fila de erro.
    /// </summary>
    public int RetryCount { get; set; } = 3;

    /// <summary>
    /// Intervalo entre tentativas de retry em segundos.
    /// </summary>
    public int RetryIntervalSeconds { get; set; } = 5;

    /// <summary>
    /// Habilita ou desabilita o filtro de logging.
    /// </summary>
    public bool EnableLogging { get; set; } = true;

    /// <summary>
    /// Prefixo para nomes de filas. Útil para ambientes diferentes.
    /// </summary>
    public string QueuePrefix { get; set; } = string.Empty;
}
