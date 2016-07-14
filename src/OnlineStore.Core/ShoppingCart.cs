using System;
using System.Linq;
using System.Collections.Generic;

namespace OnlineStore.Core
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = new List<Item>();
        }

        public int Id { get; set; }

        public IList<Item> Items { get; }

        public decimal Discount { get; set; }

        public decimal CalculateTotalAmount()
        {
            decimal totalPrice = this.Items.Sum(i => i.Price);

            return Math.Max(totalPrice - this.Discount, 0);
        }
    }
}
