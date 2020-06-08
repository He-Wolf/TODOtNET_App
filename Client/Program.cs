using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;
using TodoApi.Client.Services;

namespace blazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(sp =>
                new HttpClient
                {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                });
            
            builder.Services.AddAuthorizationCore();

            builder.Services.AddSingleton<AuthenticationStateProvider, JWTAuthStateProvider>();
            
            builder.Services.AddSingleton<IAuthService, AuthService>();

            await builder.Build().RunAsync();
        }
    }
}
