using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Nancy.Owin;

namespace OnlineStore.RestApi
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy());
        }

        public static void Main(string[] args) {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:5000")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}