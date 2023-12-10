using Core.Domain.DomainUtil;
using Core.Model.DiscountCodeModels;
using Core.Model.SearchModels;
using Core.Model.UserModels;
using Core.Repository;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.ComponentModel.DataAnnotations;
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

        [HttpGet]
        public async Task<int> NumOfDoctors()
        {
            return (await _unitOfWork.DoctorRepository.GetAllAsync()).Count();
        }

        [HttpGet]
        public async Task<int> NumOfPatients()
        {
            return (await _unitOfWork.IdentityRepository.GetAllUsersAsync()).Count(u => u.AccountType == AccountType.Patient);
        }

        [HttpPost()]
        public async Task<IActionResult> GetAllDoctors([FromBody] StringSearchModel search)
        {
            if (ModelState.IsValid)
            {
                var doctors = await _adminService.GetAllDoctorsAsync(search);
                
                return Ok(doctors);
            }

            return BadRequest("Invalid data");
        }

        [HttpPost()]
        public async Task<IActionResult> GetDoctorById([FromForm] [Required] int id)
        {
            if (ModelState.IsValid)
            {
                var doctor = await _adminService.GetDoctorByIdAsync(id);

                if (doctor != null)
                {
                    return Ok(doctor);
                }

                return BadRequest($"Doctor with Id : {id} is not found");
            }

            return BadRequest("Invalid data");
        }

        [HttpPost()]
        public async Task<IActionResult> AddDoctor([FromForm] DoctorModel model)
        {
            if (ModelState.IsValid)
            {
                // To generate the image path!
                var imagePath = ImageFileHelpers.GenerateProfileImagePath(model.ProfileImage);

                // Call the service to register the doctor as a user!
                var result = await _adminService.AddDoctorAsync(model, imagePath);

                if (result.Succeeded)
                {
                    // Save the profile image to wwwroot/images!
                    _hostingEnvironment.SaveProfileImage(model.ProfileImage, imagePath);

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
                // To get the old image path!
                var oldImagePath = await ImageFileHelpers.GetProfileImagePathAsync(_unitOfWork, model.Id);

                // To generate the new image path!
                var newImagePath = ImageFileHelpers.GenerateProfileImagePath(model.ProfileImage);

                // Call the service to edit the doctor as a user!
                var result = await _adminService.EditDoctorAsync(model, newImagePath);

                if (result.Succeeded)
                {
                    // Edit the profile image in wwwroot/images!
                    _hostingEnvironment.EditProfileImage(oldImagePath, newImagePath, model.ProfileImage);

                    return Ok("Editing Successful");
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Invalid editing data");
        }

        [HttpPost()]
        public async Task<IActionResult> DeleteDoctor([FromForm] [Required] int id)
        {
            if (ModelState.IsValid)
            {
                // To get the old image path!
                var oldImagePath = await ImageFileHelpers.GetProfileImagePathAsync(_unitOfWork, id);

                // Call the service to delete the doctor!
                var result = await _adminService.DeleteDoctorAsync(id);

                if (result.Succeeded)
                {
                    // Delete the profile image in wwwroot/images!
                    _hostingEnvironment.DeleteProfileImage(oldImagePath);

                    return Ok("Editing Successful");
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Invalid deleting data");
        }

        [HttpPost()]
        public async Task<IActionResult> GetAllPatients([FromBody] StringSearchModel search)
        {
            if (ModelState.IsValid)
            {
                var patients = await _adminService.GetAllPatientsAsync(search);

                return Ok(patients);
            }

            return BadRequest("Invalid data");
        }

        [HttpPost()]
        public async Task<IActionResult> GetPatientById([FromForm] [Required] string id)
        {
            if (ModelState.IsValid)
            {
                var patient = await _adminService.GetPatientByIdAsync(id);

                if (patient != null)
                {
                    return Ok(patient);
                }

                return BadRequest($"Patient with Id : {id} is not found");
            }

            return BadRequest("Invalid data");
        }

        [HttpPost()]
        public async Task<IActionResult> AddDiscountCodeCoupon([FromForm] AddDiscountModel model)
        {
            if (ModelState.IsValid)
            {
                await _adminService.AddDiscountCodeAsync(model);
                return Ok("Discount code coupon adding successful");
            }

            return BadRequest("Invalid data");
        }

        [HttpPost()]
        public async Task<IActionResult> UpdateDiscountCodeCoupon([FromForm] UpdateDiscountModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.UpdateDiscountCodeAsync(model);

                if (result)
                {
                    return Ok("Discount code coupon updating successful");
                }

                return BadRequest("Discount code coupon updating failed");
            }

            return BadRequest("Invalid data");
        }

        [HttpPost()]
        public async Task<IActionResult> DeleteDiscountCodeCouponById([FromForm] [Required] int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.DeleteDiscountCodeByIdAsync(id);

                if (result)
                {
                    return Ok("Discount code coupon deleting successful");
                }

                return BadRequest("Discount code coupon deleting failed");
            }

            return BadRequest("Invalid data");
        }

        [HttpPost()]
        public async Task<IActionResult> DeactivateDiscountCodeCouponById([FromForm] [Required] int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.DeactivateDiscountCodeByIdAsync(id);

                if (result)
                {
                    return Ok("Discount code coupon deactivation successful");
                }

                return BadRequest("Discount code coupon deactivation failed");
            }

            return BadRequest("Invalid data");
        }
    }
}
