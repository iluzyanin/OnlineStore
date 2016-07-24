using OnlineStore.Core.Models;

namespace OnlineStore.Core
{
    public interface IItemRepository
    {
        Item Get(int id);
        
        int Add(Item item);

        void Delete(int id);

        Item[] GetAll();
    }
}