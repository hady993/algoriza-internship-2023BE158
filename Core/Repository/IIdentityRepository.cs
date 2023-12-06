using Core.Domain;
using Microsoft.AspNetCore.Identity;

namespace Core.Repository
{
    public interface IIdentityRepository
    {
        Task<ApplicationUser> FindUserByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password, string role);
        Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
        Task<SignInResult> PasswordSignInAsync(string email, string password);
        Task SignOutAsync();
        Task<bool> IsUserInRoleAsync(string userId, string role);
    }
}
