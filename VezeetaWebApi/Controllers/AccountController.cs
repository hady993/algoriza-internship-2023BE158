using Core.Model.UserModels;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VezeetaWebApi.Controllers
{
    [Route("Vezeeta/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("Login")]
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

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _identityService.LogoutAsync();
            return Ok("Logout successful");
        }

    }
}
