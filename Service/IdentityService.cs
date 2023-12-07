using Core.Domain;
using Core.Domain.DomainUtil;
using Core.Model;
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

        public async Task<IdentityResult> RegisterUserAsync(BaseUserModel model, AccountType type, IFormFile? profileImage)
        {
            // To generate the full name!
            var fullName = model.FirstName + " " + model.LastName;

            // To generate the image path!
            var imagePath = (profileImage != null) ? "/images/" + Guid.NewGuid() + profileImage.FileName : null;

            // To generate a new user for registeration!
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.Phone,
                FullName = fullName,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                AccountType = type,
                ProfileImage = imagePath
            };

            // To generate the user role name!
            var role = Enum.GetName(typeof(AccountType), type);

            return await _identityRepository.CreateUserAsync(user, model.Password, role);
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            return await _identityRepository.PasswordSignInAsync(email, password);
        }

        public async Task LogoutAsync()
        {
            await _identityRepository.SignOutAsync();
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string role)
        {
            return await _identityRepository.IsUserInRoleAsync(userId, role);
        }
    }
}
