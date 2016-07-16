using OnlineStore.Core.Models;

namespace OnlineStore.Tests
{
    public static class TestItems
    {
        public static Item Apple => new Item { Id = 1, Name = "Apple", Price = 3 };
        public static Item Orange => new Item { Id = 2, Name = "Orange", Price = 2.8m };
        public static Item Banana => new Item { Id = 3, Name = "Banana", Price = 1.4m };
    }
}