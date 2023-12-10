using Core.Model.AppointmentModels;
using Core.Model.UserModels;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VezeetaWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = Roles.Doctor)]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctorAppointments([FromBody] AddDoctorSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _doctorService.AddAppointmentAsync(model);

                if (result)
                {
                    return Ok("Doctor's appointments adding successful");
                }

                return BadRequest("Doctor's appointments adding failed");
            }

            return BadRequest("Invalid data");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDoctorTime([FromForm] UpdateDoctorSettingModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _doctorService.UpdateTimeAsync(model);

                if (result)
                {
                    return Ok("Doctor's time updating successful");
                }

                return BadRequest("Doctor's time updating failed");
            }

            return BadRequest("Invalid data");
        }
    }
}
