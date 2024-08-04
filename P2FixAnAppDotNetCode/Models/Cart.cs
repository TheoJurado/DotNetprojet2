using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        // for OrderLineId to always be unique
        public static int nextOrderLineId = 0;

        private List<CartLine> cartLines = new List<CartLine>();

        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => cartLines;

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            // TODO implement the method//
            // if the product we are looking to add is already present in the cart
            if (FindProductInCartLines(product.Id) != null)
            {
                //add "quantity" to his quantity
                GetCartLineByProduct(product.Id).Quantity += quantity;
            }
            else
            {
                //add a new cartLine for this product
                CartLine newLine = new CartLine
                {
                    OrderLineId = nextOrderLineId,
                    Product = product,
                    Quantity = quantity
                };
                nextOrderLineId++;
                cartLines.Add(newLine);
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            cartLines.RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            // TODO implement the method//
            double totalPrice = 0.0;
            foreach (CartLine line in cartLines)
            {
                totalPrice += (line.Product.Price * line.Quantity);
            }
            return totalPrice;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            // TODO implement the method//
            int totalQuantity = 0;
            foreach (CartLine line in cartLines)
            {
                totalQuantity += line.Quantity;
            }

            if (totalQuantity > 0)
                return GetTotalValue() / totalQuantity;
            else
                return 0.0;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // TODO implement the method//
            CartLine line = GetCartLineByProduct(productId);

            if (line != null)
                return line.Product;
            else
                return null;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Get a specific cartline by a product
        /// </summary>
        public CartLine GetCartLineByProduct(int productId)
        {
            // TOCREAT implement the method//
            foreach (CartLine line in cartLines)
            {
                if (line.Product.Id == productId)
                {
                    return line;
                }
            }

            return null;
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
