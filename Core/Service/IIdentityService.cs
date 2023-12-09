using Core.Domain;
using Core.Domain.DomainUtil;
using Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IIdentityService
    {
        Task<IdentityResult> RegisterUserAsync(BaseUserModel model, AccountType type, string? imagePath);
        Task<IdentityResult> UpdateUserAsync(SuperBaseUserModel model, string? imagePath, ApplicationUser user);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
        Task<IdentityResult> ChangeUserPasswordAsync(ApplicationUser user, string oldPassword, string newPassword);
        Task<SignInResult> LoginAsync(string email, string password);
        Task LogoutAsync();
        Task<bool> IsUserInRoleAsync(string userId, string role);
    }
}
