using Core.Model;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VezeetaWebApi.Controllers
{
    [Route("Vezeeta/[controller]/[action]")]
    [Authorize(Roles = Roles.Patient)]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PatientController(IPatientService patientService, IWebHostEnvironment hostingEnvironment)
        {
            _patientService = patientService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Call the service to register the patient as a user!
                var result = await _patientService.RegisterPatientAsync(model);

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
