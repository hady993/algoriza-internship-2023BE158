using Core.Model;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VezeetaWebApi.Util;

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
                // To generate the image path!
                var imagePath = ImageFileHelpers.GenerateProfileImagePath(model.ProfileImage);

                // Call the service to register the patient as a user!
                var result = await _patientService.RegisterPatientAsync(model, imagePath);

                if (result.Succeeded)
                {
                    // Save the profile image to wwwroot/images if the path != null!
                    if (model.ProfileImage != null)
                    {
                        _hostingEnvironment.SaveProfileImage(model.ProfileImage, imagePath);
                    }

                    return Ok("Registration successful");
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Invalid registration data");
        }

    }
}
