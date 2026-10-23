using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Web.Components;
using Web.Services;
using Web.Services.Interface;

namespace Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddMudServices();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IApplicationNavigation, ApplicationNavigation>();

            // backend
            builder.Services.AddHttpClient("backend", client =>
            {
                //client.BaseAddress = new Uri("https://acara.cyou.somee.com");
                client.BaseAddress = new Uri("http://localhost:8081");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            await builder.Build().RunAsync();
        }
    }
}
