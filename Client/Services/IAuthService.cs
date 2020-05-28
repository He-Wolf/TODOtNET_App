using TodoApi.Shared.Models;
using System.Threading.Tasks;

namespace TodoApi.Client.Services
{
    public interface IAuthService
    {
        Task<TokenViewModel> Register(RegisterViewModel registerModel);
        Task<TokenViewModel> Login(LoginViewModel loginModel);
        Task Logout();
    }
}