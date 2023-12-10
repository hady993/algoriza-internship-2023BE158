using Core.Model.BookingModels;
using Core.Model.SearchModels;
using Core.Model.UserModels;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
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

        [HttpPost]
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

        [HttpPost]
        public async Task<IActionResult> GetAllDoctors([FromBody] StringSearchModel search)
        {
            if (ModelState.IsValid)
            {
                var doctors = await _patientService.GetAllDoctorsAsync(search);

                return Ok(doctors);
            }

            return BadRequest("Invalid data");
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] BookingModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _patientService.AddBookingAsync(model);

                if (result)
                {
                    return Ok("Adding booking successful");
                }

                return BadRequest("Adding booking failed");
            }

            return BadRequest("Invalid data");
        }

        [HttpPost]
        public async Task<IActionResult> CancelBooking([FromBody] BookingCancelModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _patientService.CancelBookingAsync(model);

                if (result)
                {
                    return Ok("Cancel booking successful");
                }

                return BadRequest("Cancel booking failed");
            }

            return BadRequest("Invalid data");
        }
    }
}
