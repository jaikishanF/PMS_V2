using Application.Interfaces;
using Application.ViewModels;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                StreetNameAndNumber = model.StreetNameAndNumber,
                City = model.City,
                PostalCode = model.PostalCode,
                Country = model.Country,
                EmergencyDetails = new EmergencyDetails
                {
                    EmergencyName = model.EmergencyDetails.EmergencyName,
                    PhoneNumber = model.EmergencyDetails.PhoneNumber
                },
                InsuranceDetails = new InsuranceDetails
                {
                    Provider = model.InsuranceDetails.Provider,
                    PolicyNumber = model.InsuranceDetails.PolicyNumber
                }
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            return result.Succeeded;
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
