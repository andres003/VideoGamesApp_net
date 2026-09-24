using VideoGamesApp.Domain.Enums;

namespace VideoGamesApp.Application.DTOs
{
    public record OrderItemResponseDto(
    Guid VideoGameId,
    string VideoGameTitle,
    int Quantity,
    decimal UnitPrice,
    decimal SubTotal
);

    public record OrderResponseDto(
        Guid Id,
        string CustomerEmail,
        DateTime OrderDate,
        OrderStatus Status,
        bool IsPreOrder,
        decimal TotalAmount,
        List<OrderItemResponseDto> Items
    );
}
