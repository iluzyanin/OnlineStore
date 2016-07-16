namespace OnlineStore.Core.Models
{
    public class CartItem
    {
        public CartItem() { }

        public CartItem(int id, Item item, int quantity)
        {
            this.Id = id;
            this.Item = item;
            this.Quantity = quantity;
        }

        public int Id { get; set; }

        public Item Item { get; set; }

        public int Quantity { get; set; }
    }

}
