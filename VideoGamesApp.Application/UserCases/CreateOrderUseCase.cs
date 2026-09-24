using VideoGamesApp.Application.DTOs;
using VideoGamesApp.Application.Interfaces;
using VideoGamesApp.Domain.Entities;
using VideoGamesApp.Domain.Exceptions;
using VideoGamesApp.Domain.Interfaces;

namespace VideoGamesApp.Application.UserCases
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IVideoGameRepository _videoGameRepository;

        public CreateOrderUseCase(
        IOrderRepository orderRepository,
        IVideoGameRepository videoGameRepository)
        {
            _orderRepository = orderRepository;
            _videoGameRepository = videoGameRepository;
        }
        public async Task<OrderResponseDto> ExecuteAsync(CreateOrderDto dto)
        {
            // 1. Instanciamos la Entidad Orden
            var order = new Order(dto.CustomerEmail, dto.IsPreOrder);

            // 2. Procesamos cada ítem recibido
            foreach (var itemDto in dto.Items)
            {
                var game = await _videoGameRepository.GetByIdAsync(itemDto.VideoGameId);
                if (game == null) throw new DomainException($"VideoGame with ID '{itemDto.VideoGameId}'");

                order.AddItem(game, itemDto.Quantity);
                await _videoGameRepository.UpdateAsync(game);
            }

            order.CompleteOrder();
            await _orderRepository.AddAsync(order);

            return new OrderResponseDto(
                    order.Id,
                    order.CustomerEmail,
                    order.OrderDate,
                    order.Status,
                    order.IsPreOrder,
                    order.TotalAmount,
                    order.Items.Select(i => new OrderItemResponseDto(
                            i.VideoGameId,
                            i.VideoGameTitle,
                            i.Quantity,
                            i.UnitPrice,
                            i.SubTotal
                        )).ToList()

                );
        }
    }
}
