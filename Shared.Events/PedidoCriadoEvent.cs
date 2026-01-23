using BuildingBlocks.Messaging.Events;

namespace Shared.Events;

/// <summary>
/// Evento de integração que representa a criação de um pedido.
/// Este evento é publicado pelo Producer e consumido pelo Consumer.
/// </summary>
public record PedidoCriadoEvent : IntegrationEvent
{
    /// <summary>
    /// Identificador único do pedido.
    /// </summary>
    public Guid PedidoId { get; init; }

    /// <summary>
    /// Nome do cliente que realizou o pedido.
    /// </summary>
    public string NomeCliente { get; init; } = string.Empty;

    /// <summary>
    /// Valor total do pedido.
    /// </summary>
    public decimal ValorTotal { get; init; }

    /// <summary>
    /// Data e hora em que o pedido foi criado.
    /// </summary>
    public DateTime DataPedido { get; init; }

    /// <summary>
    /// Lista de itens do pedido.
    /// </summary>
    public List<ItemPedido> Itens { get; init; } = new();
}

/// <summary>
/// Representa um item do pedido.
/// </summary>
public record ItemPedido
{
    /// <summary>
    /// Nome do produto.
    /// </summary>
    public string NomeProduto { get; init; } = string.Empty;

    /// <summary>
    /// Quantidade do produto.
    /// </summary>
    public int Quantidade { get; init; }

    /// <summary>
    /// Preço unitário do produto.
    /// </summary>
    public decimal PrecoUnitario { get; init; }
}
