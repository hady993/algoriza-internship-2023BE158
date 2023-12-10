using Core.Domain;
using Core.Domain.DomainUtil;
using Core.Model.UserModels;
using Core.Repository;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Service
{
    public class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> RegisterUserAsync(BaseUserModel model, AccountType type, string? imagePath)
        {
            // To generate the full name!
            var fullName = model.FirstName + " " + model.LastName;

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

            return await _unitOfWork.IdentityRepository.CreateUserAsync(user, model.Password, role);
        }

        public async Task<IdentityResult> UpdateUserAsync(SuperBaseUserModel model, string? imagePath, ApplicationUser user)
        {
            // To generate the full name!
            var fullName = model.FirstName + " " + model.LastName;

            // To update user's properties!
            user.UserName = model.Email;
            user.Email = model.Email;
            user.PhoneNumber = model.Phone;
            user.FullName = fullName;
            user.Gender = model.Gender;
            user.DateOfBirth = model.DateOfBirth;
            user.ProfileImage = imagePath;

            return await _unitOfWork.IdentityRepository.UpdateUserAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            return await _unitOfWork.IdentityRepository.DeleteUserAsync(user);
        }

        public async Task<IdentityResult> ChangeUserPasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            return await _unitOfWork.IdentityRepository.UpdatePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            return await _unitOfWork.IdentityRepository.PasswordSignInAsync(email, password);
        }

        public async Task LogoutAsync()
        {
            await _unitOfWork.IdentityRepository.SignOutAsync();
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string role)
        {
            return await _unitOfWork.IdentityRepository.IsUserInRoleAsync(userId, role);
        }
    }
}
