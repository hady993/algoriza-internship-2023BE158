using Core.Model;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace VezeetaWebApi.Controllers
{
    [Route("Vezeeta/[controller]/[action]")]
    [Authorize(Roles = Roles.Admin)]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminController(IAdminService adminService, IWebHostEnvironment hostingEnvironment)
        {
            _adminService = adminService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost()]
        public async Task<IActionResult> AddDoctor([FromForm] DoctorModel model)
        {
            if (ModelState.IsValid)
            {
                // Call the service to register the patient as a user!
                var result = await _adminService.AddDoctorAsync(model);

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
