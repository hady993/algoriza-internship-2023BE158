using Core.Model;
using Core.Repository;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using VezeetaWebApi.Util;

namespace VezeetaWebApi.Controllers
{
    [Route("Vezeeta/[controller]/[action]")]
    [Authorize(Roles = Roles.Admin)]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdminService _adminService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminController(IUnitOfWork unitOfWork, IAdminService adminService, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _adminService = adminService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost()]
        public async Task<IActionResult> AddDoctor([FromForm] DoctorModel model)
        {
            if (ModelState.IsValid)
            {
                // Call the service to register the doctor as a user!
                var result = await _adminService.AddDoctorAsync(model);

                if (result.Succeeded)
                {
                    // Save the profile image to wwwroot/images!
                    _hostingEnvironment.SaveProfileImage(model.ProfileImage);

                    return Ok("Registration successful");
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Invalid registration data");
        }

        [HttpPost()]
        public async Task<IActionResult> EditDoctor([FromForm] DoctorUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                // Call the service to edit the doctor as a user!
                var result = await _adminService.EditDoctorAsync(model);

                if (result.Succeeded)
                {
                    // Edit the profile image in wwwroot/images!
                    var imgPath = await _unitOfWork.GetProfileImagePathAsync(model.Id);
                    _hostingEnvironment.EditProfileImage(imgPath, model.ProfileImage);

                    return Ok("Editing Successful");
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Invalid editing data");
        }

    }
}
