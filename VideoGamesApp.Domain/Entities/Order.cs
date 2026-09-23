using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesApp.Domain.Enums;
using VideoGamesApp.Domain.Exceptions;

namespace VideoGamesApp.Domain.Entities
{
    public class Order
    {
        private readonly List<OrderItem> _items = new();
        public Guid Id { get; private set; }
        public string CustomerEmail { get; private set; } = string.Empty;
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public bool IsPreOrder { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public decimal TotalAmount => _items.Sum(item => item.SubTotal);

        private Order() { }

        public Order(string customerEmail, bool isPreOrder = false)
        {
            if (string.IsNullOrWhiteSpace(customerEmail) || !customerEmail.Contains("@"))
                throw new DomainException("A valid customer email is required.");

            Id = Guid.NewGuid();
            CustomerEmail = customerEmail;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
            IsPreOrder = isPreOrder;
        }

        // Regla de Negocio: Agregar un producto a la orden
        public void AddItem(VideoGame game, int quantity)
        {
            if (Status != OrderStatus.Pending)
                throw new DomainException("Cannot add items to a completed or cancelled order.");

            if (IsPreOrder && !game.IsPreOrder)
                throw new DomainException("Cannot mix regular games in a pre-order transaction.");

            if (!IsPreOrder && game.IsPreOrder)
                throw new DomainException("Cannot add a pre-order game to a regular order.");

            // Descontamos el stock si es una compra inmediata
            if (!game.IsPreOrder)
            {
                game.ReduceStock(quantity);
            }

            _items.Add(new OrderItem(game.Id, game.Title, quantity, game.Price));
        }

        // Regla de Negocio: Completar la orden
        public void CompleteOrder()
        {
            if (_items.Count() == 0)
                throw new DomainException("Cannot complete an empty order.");

            if (Status != OrderStatus.Pending)
                throw new DomainException("Order is not in pending status.");

            Status = OrderStatus.Completed;
        }

        // Regla de Negocio: Cancelar la orden
        public void CancelOrder()
        {
            if (Status == OrderStatus.Completed)
                throw new DomainException("Cannot cancel an already completed order.");

            Status = OrderStatus.Cancelled;
        }

    }
}
