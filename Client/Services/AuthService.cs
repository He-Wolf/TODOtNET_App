using TodoApi.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TodoApi.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly JWTAuthStateProvider _authenticationStateProvider;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider as JWTAuthStateProvider;
        }

        public async Task<TokenViewModel> Register(RegisterViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync<RegisterViewModel>("/Register", model);
            var content = await response.Content.ReadFromJsonAsync<TokenViewModel>();
            
            await _authenticationStateProvider.SetTokenAsync(content.token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", content.token);

            return content;
        }

        public async Task<TokenViewModel> Login(LoginViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync<LoginViewModel>("/Login", model);
            var content = await response.Content.ReadFromJsonAsync<TokenViewModel>();

            await _authenticationStateProvider.SetTokenAsync(content.token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", content.token);

            return content;
        }

        public async Task Logout()
        {
            await _authenticationStateProvider.SetTokenAsync(null);
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}