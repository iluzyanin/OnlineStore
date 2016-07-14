using Nancy;

namespace OnlineStore.RestApi
{
    public class ShoppingCartModule : NancyModule
    {
        public ShoppingCartModule()
        {
            Get("/", _ => "Stub for GET All endpoint");
        }
    }
}
