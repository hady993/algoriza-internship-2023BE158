using Core.Model.AppointmentModels;
using Core.Model.BookingModels;
using Core.Model.UserModels;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> ConfirmCheckUp([FromForm] ConfirmCheckupModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _doctorService.ConfirmCheckUpAsync(model);

                if (result)
                {
                    return Ok("Confirming checkup successful");
                }

                return BadRequest("Confirming checkup failed");
            }

            return BadRequest("Invalid data");
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

        [HttpPost]
        public async Task<IActionResult> DeleteDoctorTime([FromForm] DeleteDoctorTimeModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _doctorService.DeleteTimeAsync(model);

                if (result)
                {
                    return Ok("Doctor's time deleting successful");
                }

                return BadRequest("Doctor's time deleting failed");
            }

            return BadRequest("Invalid data");
        }
    }
}
