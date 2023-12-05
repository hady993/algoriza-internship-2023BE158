using Core.Model;
using Core.Model.ModelUtil;
using Core.Repository;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Service
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _identityRepository;

        public IdentityService(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task<IdentityResult> RegisterUserAsync(string firstName, string lastName,string email, string password, 
            string phone, Gender gender, DateOnly dateOfBirth, AccountType accountType, string? imagePath)
        {
            // To generate the full name!
            var fullName = firstName + " " + lastName;

            // To generate a new user for registeration!
            var user = new ApplicationUser 
            { 
                UserName = email, Email = email, PhoneNumber = phone,
                FullName = fullName, Gender = gender, DateOfBirth = dateOfBirth,
                AccountType = accountType, ProfileImage = imagePath
            };

            return await _identityRepository.CreateUserAsync(user, password);
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            return await _identityRepository.PasswordSignInAsync(email, password);
        }

        public async Task LogoutAsync()
        {
            await _identityRepository.SignOutAsync();
        }
    }
}
