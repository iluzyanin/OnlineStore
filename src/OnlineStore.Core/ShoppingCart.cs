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

        public IList<Item> Items { get; set; }
    }
}
