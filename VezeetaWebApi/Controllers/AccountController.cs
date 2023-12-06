using Core.Model;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VezeetaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AccountController(IIdentityService identityService, IWebHostEnvironment hostingEnvironment)
        {
            _identityService = identityService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Call the identity service to register the user!
                var result = await _identityService.RegisterUserAsync(model);

                // Save the profile image to wwwroot/images if the path != null!
                if (model.ProfileImage != null)
                {
                    // Helper method for saving files!
                    SaveProfileImage(model.ProfileImage);
                }

                if (result.Succeeded)
                {
                    return Ok("Registration successful");
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Invalid registration data");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Call the identity service to authenticate the user using Email and Password!
                var result = await _identityService.LoginAsync(model.Email, model.Password);

                if (result.Succeeded)
                {
                    return Ok("Login successful");
                }

                return BadRequest("Invalid login attempt");
            }

            return BadRequest("Invalid login data");
        }

        [HttpPost("logout")]
        [Authorize(Roles = Roles.Admin)] // Ensure that only authenticated users can log out
        public async Task<IActionResult> Logout()
        {
            await _identityService.LogoutAsync();
            return Ok("Logout successful");
        }

        // Helper method to save profile image to wwwroot/images
        private void SaveProfileImage(IFormFile profileImage)
        {
            var uploadsDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "images");

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            var uniqueFileName = Guid.NewGuid() + profileImage.FileName;
            var filePath = Path.Combine(uploadsDirectory, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                profileImage.CopyTo(fileStream);
            }
        }

    }
}
