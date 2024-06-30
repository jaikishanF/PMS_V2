using Application.ViewModels;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterViewModel model);
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}
