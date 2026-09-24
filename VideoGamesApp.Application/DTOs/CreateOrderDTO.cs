namespace VideoGamesApp.Application.DTOs
{
    public record OrderItemDto(
    Guid VideoGameId,
    int Quantity
);

    public record CreateOrderDto(
        string CustomerEmail,
        bool IsPreOrder,
        List<OrderItemDto> Items
    );
}
