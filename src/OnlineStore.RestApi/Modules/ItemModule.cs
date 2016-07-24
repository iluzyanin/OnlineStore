using System;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Nancy.Responses.Negotiation;
using OnlineStore.Core;
using OnlineStore.Core.Models;
using OnlineStore.RestApi.DataTransfer;

namespace OnlineStore.RestApi.Modules
{
    public class ItemModule : NancyModule
    {
        private IItemRepository itemRepository;

        public ItemModule(IItemRepository itemRepository)
            : base("/items")
        {
            this.itemRepository = itemRepository;

            this.Get("/", _ => GetAllItems());
            this.Get("/{id}", parameters => GetItem(parameters.id));
            this.Post("/", parameters => CreateItem(parameters));
            this.Delete("/{id}", parameters => DeleteItem(parameters.id));
        }

        public Negotiator GetAllItems()
        {
            return Negotiate
                .WithAllowedMediaRange("application/json")
                .WithModel(this.itemRepository.GetAll());
        }

        public Negotiator GetItem(int id)
        {
            Item item = this.itemRepository.Get(id);
            if (item == null)
            {
                return Negotiate
                    .WithAllowedMediaRange("application/json")
                    .WithModel(new { Error = $"Item with id = {id} was not found" })
                    .WithStatusCode(HttpStatusCode.NotFound);
            }

            return Negotiate
                .WithAllowedMediaRange("application/json")
                .WithModel(item);
        }

        public Negotiator CreateItem(dynamic parameters)
        {
            NewItemDto newItem = this.BindAndValidate<NewItemDto>();

            if (!ModelValidationResult.IsValid)
            {
                return Negotiate
                    .WithModel(ModelValidationResult.Errors)
                    .WithStatusCode(HttpStatusCode.BadRequest);
            }

            Item existingItem = this.itemRepository
                .GetAll()
                .FirstOrDefault(i => i.Name.Equals(newItem.Name, StringComparison.CurrentCultureIgnoreCase));
            if (existingItem != null)
            {
                return Negotiate
                    .WithAllowedMediaRange("application/json")
                    .WithModel(new { Error = $"Item with same name ({newItem.Name}) already exists" })
                    .WithStatusCode(HttpStatusCode.BadRequest);
            }

            int newId = this.itemRepository.Add(new Item(newItem.Name, newItem.Price));

            return Negotiate
                .WithHeader("Location", $"{Request.Url}/{newId}")
                .WithStatusCode(HttpStatusCode.Created);
        }

        public Negotiator DeleteItem(int id)
        {
            Item item = this.itemRepository.GetAll().FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return Negotiate
                    .WithAllowedMediaRange("application/json")
                    .WithModel(new { Error = $"Item with id = {id} was not found" })
                    .WithStatusCode(HttpStatusCode.NotFound);
            }

            this.itemRepository.Delete(id);

            return Negotiate
                .WithAllowedMediaRange("application/json")
                .WithStatusCode(HttpStatusCode.NoContent);
        }
    }
}