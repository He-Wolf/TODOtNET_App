using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthStateProvider>();
            
            builder.Services.AddScoped<IAuthService, AuthService>();

            await builder.Build().RunAsync();
        }
    }
}
