public class Item
{
    public Item() {}

    public Item(int id, string name, decimal price)
    {
        this.Id = id;
        this.Name = name;
        this.Price = price;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }
}