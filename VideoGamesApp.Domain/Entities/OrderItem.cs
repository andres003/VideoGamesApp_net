using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesApp.Domain.Exceptions;

namespace VideoGamesApp.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid VideoGameId { get; private set; }
        public string VideoGameTitle { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal SubTotal => Quantity * UnitPrice;

        private OrderItem() { }
        public OrderItem(Guid videoGameId, string videoGameTitle, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            if (unitPrice <= 0)
                throw new DomainException("Unit price must be greater than zero.");

            Id = Guid.NewGuid();
            VideoGameId = videoGameId;
            VideoGameTitle = videoGameTitle;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
