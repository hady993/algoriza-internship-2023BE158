﻿using Core.Domain;
using Core.Repository;
using Microsoft.AspNetCore.Identity;

namespace Repository
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityRepository(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        // To support Registeration!
        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // To add the role automatically during the registeration!
                return await AddUserToRoleAsync(user, role);
            }

            return result;
        }

        public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role)
        {
            try
            {
                // Add the role to the user!
                return await _userManager.AddToRoleAsync(user, role);
            }
            catch (Exception ex)
            {
                // Remove the added user when the role adding failed!
                await DeleteUserAsync(user);

                // Error of login failure according to adding role failure!
                var error = new IdentityError();
                error.Description = "Login failed according to adding role failure";
                return IdentityResult.Failed(error);
            }
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            // Remove the registered user!
            return await _userManager.DeleteAsync(user);
        }
        
        // To support Login!
        public async Task<SignInResult> PasswordSignInAsync(string email, string password)
        {
            return await _signInManager.PasswordSignInAsync(email, password, false, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            return await _userManager.IsInRoleAsync(user, role);
        }
    }
}
