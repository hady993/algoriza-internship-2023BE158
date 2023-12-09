using Core.Domain;
using Core.Domain.DomainUtil;
using Core.Helpful;
using Core.Model;
using Core.Repository;
using Core.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AdminService : IAdminService
    {
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityRepository _identityRepository;

        public AdminService(IIdentityService identityService, IUnitOfWork unitOfWork, IIdentityRepository identityRepository)
        {
            _identityService = identityService;
            _unitOfWork = unitOfWork;
            _identityRepository = identityRepository;
        }

        public async Task<IdentityResult> AddDoctorAsync(DoctorModel model)
        {
            // To register the doctor as a user!
            var result = await _identityService.RegisterUserAsync(model, AccountType.Doctor, model.ProfileImage);

            // Add doctor's specialization if the registeration was succeeded!
            if (result.Succeeded)
            {
                var doctor = new Doctor
                {
                    UserId = _identityRepository.FindUserByEmailAsync(model.Email).Result.Id,
                    SpecializationId = (int) model.SpecializationType,
                    Price = 20.0
                };

                await _unitOfWork.DoctorRepository.AddEntityAsync(doctor);
                _unitOfWork.Complete();
            }

            return result;
        }

        public async Task<IdentityResult> EditDoctorAsync(DoctorUpdateModel model)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetEntityByIdAsync(model.Id, includeProperties: "User,Appointments");

            // Check if the doctor exists!
            if (doctor == null)
            {
                // Error of editing failure (doctor is not found)!
                return HelpfulMessages.IdentityResultError($"Edit failed (Doctor of Id : {model.Id} is not found)");
            }

            // Check if the doctor is booked!
            var bookings = await _unitOfWork.BookingRepository.GetAllAsync();

            if (bookings.Any())
            {
                var isBooked = bookings.Any(b => b.Time.Appointment.DoctorId == model.Id && b.Status != BookingStatus.Cancelled);

                if (isBooked)
                {
                    // Error of editing failure (doctor is booked)!
                    return HelpfulMessages.IdentityResultError($"Edit failed (Doctor of Id : {model.Id} is booked)");
                }
            }

            // To edit doctor's specialization!
            doctor.SpecializationId = (int) model.SpecializationType;
            await _unitOfWork.DoctorRepository.EditEntityAsync(doctor, doctor.Id);

            // To edit doctor's user account!
            var result = await _identityService.UpdateUserAsync(model, model.ProfileImage, doctor.User);

            if (result.Succeeded)
            {
                // To edit doctor's password if exists!
                if (model.OldPassword != null && model.Password != null)
                    return await _identityService.ChangeUserPasswordAsync(doctor.User, model.OldPassword, model.Password);
            }

            return result;
        }
    }
}
