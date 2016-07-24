using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStore.Core.Models;

namespace OnlineStore.Core
{
    public class InMemoryItemRepository: IItemRepository
    {
        private IList<Item> items = new List<Item>();
        private int currentIndex = 1;

        public int Add(Item item)
        {
            item.Id = currentIndex++;
            items.Add(item);

            return item.Id;
        }

        public Item Get(int id)
        {
            return this.items.FirstOrDefault(i => i.Id == id);
        }

        public Item[] GetAll()
        {
            return items.ToArray();
        }

        public void Delete(int id)
        {
            var item = this.items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                this.items.Remove(item);
            }
        }
    }
}