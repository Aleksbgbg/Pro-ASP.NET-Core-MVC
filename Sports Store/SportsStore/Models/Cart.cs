namespace SportsStore.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Cart
    {
        private readonly List<CartLine> _cartLines = new List<CartLine>();

        public virtual IEnumerable<CartLine> CartLines => _cartLines;

        public virtual decimal TotalCost => _cartLines.Sum(cartLine => cartLine.Product.Price * cartLine.Quantity);

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine targetLine = _cartLines.FirstOrDefault(cartLine => cartLine.Product.Id == product.Id);

            if (targetLine == null)
            {
                _cartLines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                targetLine.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product)
        {
            _cartLines.RemoveAll(cartLine => cartLine.Product.Id == product.Id);
        }

        public virtual void Clear()
        {
            _cartLines.Clear();
        }
    }
}