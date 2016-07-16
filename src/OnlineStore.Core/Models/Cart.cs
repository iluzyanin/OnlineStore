using System;
using System.Linq;
using System.Collections.Generic;

namespace OnlineStore.Core.Models
{
    public class Cart
    {
        private IList<CartItem> cartItems;
        private int cartItemIndex;

        public Cart()
        {
            this.cartItems = new List<CartItem>();
            this.cartItemIndex = 1;
        }

        public int Id { get; set; }

        public IList<CartItem> CartItems
        {
            get
            {
                return this.cartItems.ToList();
            }
        }

        public void AddItem(Item item, int quantity)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (quantity == 0)
            {
                return;
            }

            CartItem existingCartItem = this.cartItems.FirstOrDefault(ci => ci.Item.Id == item.Id);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }
            else
            {
                this.cartItems.Add(new CartItem(cartItemIndex++, item, quantity));
            }
        }

        public decimal Discount { get; set; }

        public decimal CalculateTotalAmount()
        {
            decimal totalPrice = this.cartItems.Sum(ci => ci.Item.Price * ci.Quantity);

            return Math.Max(totalPrice - this.Discount, 0);
        }
    }
}
