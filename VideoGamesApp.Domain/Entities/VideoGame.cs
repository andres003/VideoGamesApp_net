using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGamesApp.Domain.Enums;
using VideoGamesApp.Domain.Exceptions;

namespace VideoGamesApp.Domain.Entities
{
    public class VideoGame
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Platform { get; private set; } = string.Empty; // e.g., "PC", "PS5", "Xbox Series X"
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public VideoGameGenre Genre { get; private set; }
        public bool IsPreOrder { get; private set; }
        public DateTime? ReleaseDate { get; private set; }

        private VideoGame() { }

        public VideoGame(string title, string platform, decimal price, int stock, VideoGameGenre genre, bool isPreOrder = false, DateTime? releaseDate = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title cannot be empty.");

            if (price <= 0)
                throw new DomainException("Price must be greater than zero.");

            if (price > 350000)
                throw new DomainException("The price must be less than 350k");

            if (isPreOrder && (!releaseDate.HasValue || releaseDate.Value <= DateTime.UtcNow))
                throw new DomainException("Pre-order games must have a future release date.");

            Id = Guid.NewGuid();
            Title = title;
            Platform = platform;
            Price = price;
            Stock = isPreOrder ? 0 : stock; // Las preventas no tienen stock físico inmediato
            Genre = genre;
            IsPreOrder = isPreOrder;
            ReleaseDate = releaseDate;
        }

        // Comportamiento de Negocio: Reducir Stock
        public void ReduceStock(int quantity)
        {
            if (IsPreOrder)
                throw new DomainException("Cannot reduce physical stock for a pre-order game.");

            if (quantity <= 0)
                throw new DomainException("Quantity to reduce must be positive.");

            if (Stock < quantity)
                throw new DomainException($"Not enough stock for '{Title}'. Available: {Stock}, Requested: {quantity}.");

            Stock -= quantity;
        }

        // Comportamiento de Negocio: Actualizar Precio
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new DomainException("Price must be greater than zero.");

            Price = newPrice;
        }


    }

   
}
