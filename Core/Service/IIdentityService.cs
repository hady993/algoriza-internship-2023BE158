using Core.Model.ModelUtil;
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
        Task<IdentityResult> RegisterUserAsync(string firstName, string lastName, string email, string password, 
            string phone, Gender gender, DateOnly dateOfBirth, AccountType accountType, string? imagePath);
        Task<SignInResult> LoginAsync(string email, string password);
        Task LogoutAsync();
    }
}
