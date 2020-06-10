using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TodoApi.Client.Services
{
    public class JWTAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;

        public JWTAuthStateProvider(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }
        
        public async Task<string> GetTokenAsync()
            => await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

        public async Task SetTokenAsync(string token)
        {
            if (token == null)
            {
                await _jsRuntime.InvokeAsync<object>("localStorage.removeItem", "authToken");
            }
            else
            {
                await _jsRuntime.InvokeAsync<object>("localStorage.setItem", "authToken", token);
            }
            
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await GetTokenAsync();
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            //Checks if token present
            if(string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(user);
            }

            var claims = ParseClaimsFromJwt(token);
            // Checks the exp field of the token
            var expiry = claims.Where(claim => claim.Type.Equals("exp")).FirstOrDefault();
            if (expiry == null)
            {
                return new AuthenticationState(user);
            }

            // The exp field is in Unix time
            var datetime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiry.Value));

            if (datetime.UtcDateTime <= DateTime.UtcNow)
            {
                return new AuthenticationState(user);
            }
                
            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            
            return new AuthenticationState(user);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}