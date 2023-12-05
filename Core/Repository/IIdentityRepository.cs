using Core.Model;
using Microsoft.AspNetCore.Identity;

namespace Core.Repository
{
    public interface IIdentityRepository
    {
        Task<ApplicationUser> FindUserByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<SignInResult> PasswordSignInAsync(string email, string password);
        Task SignOutAsync();
    }
}
