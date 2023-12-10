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
                var result = await _doctorService.AddAppointment(model);

                if (result)
                {
                    return Ok("Doctor's appointments adding successful");
                }

                return BadRequest("Doctor's appointments adding failed");
            }

            return BadRequest("Invalid data");
        }
    }
}
