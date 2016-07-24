using Nancy;
using Nancy.Configuration;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using OnlineStore.Core;
using OnlineStore.Core.Models;

namespace OnlineStore.RestApi
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            var itemRepository = new InMemoryItemRepository();
            itemRepository.Add(new Item("Apple", 3m));
            itemRepository.Add(new Item("Banana", 1.4m));
            itemRepository.Add(new Item("Orange", 2.8m));

            container.Register<IItemRepository, InMemoryItemRepository>(itemRepository);
        }

        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(enabled: false, displayErrorTraces: false);
        }
    }
}