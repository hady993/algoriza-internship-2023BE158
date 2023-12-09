using Core.Domain;
using Core.Domain.DomainUtil;
using Core.Helpful;
using Core.Model.DTOs;
using Core.Model.SearchModels;
using Core.Model.UserModels;
using Core.Repository;
using Core.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync(StringSearchModel searchModel)
        {
            // Retrieving all doctors from the database!
            var doctors = await _unitOfWork.DoctorRepository.GetAllAsync(includeProperties: "User,Specialization");

            // Create new list of doctorDto to retrieve the data!
            List<DoctorDto> result = new List<DoctorDto>();

            foreach (var doctor in doctors)
            {
                var doctorDto = new DoctorDto
                {
                    ProfileImage = doctor.User.ProfileImage,
                    FullName = doctor.User.FullName,
                    Email = doctor.User.Email,
                    Phone = doctor.User.PhoneNumber,
                    Gender = doctor.User.Gender.ToString(),
                    DateOfBirth = doctor.User.DateOfBirth,
                    SpetializationAr = doctor.Specialization.NameAr,
                    SpetializationEn = doctor.Specialization.NameEn
                };

                result.Add(doctorDto);
            }

            // To filter doctors and support pagination!
            var itemsToSkip = (searchModel.PageNumber - 1) * searchModel.PageSize;

            var filteredResult = result
                .Where(d => d.FullName.Contains(searchModel.Search) || d.Email.Contains(searchModel.Search))
                .Skip(itemsToSkip)
                .Take(searchModel.PageSize);

            return filteredResult;
        }

        public async Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            // Retrieving the doctor from the database!
            var doctor = await _unitOfWork.DoctorRepository.GetEntityByIdAsync(id, includeProperties: "User,Specialization");

            if (doctor == null)
                return null;

            // Create new doctorDto to retrieve the data!
            var doctorDto = new DoctorDto
            {
                ProfileImage = doctor.User.ProfileImage,
                FullName = doctor.User.FullName,
                Email = doctor.User.Email,
                Phone = doctor.User.PhoneNumber,
                Gender = doctor.User.Gender.ToString(),
                DateOfBirth = doctor.User.DateOfBirth,
                SpetializationAr = doctor.Specialization.NameAr,
                SpetializationEn = doctor.Specialization.NameEn
            };

            return doctorDto;
        }

        public async Task<IdentityResult> AddDoctorAsync(DoctorModel model, string? imagePath)
        {
            // To register the doctor as a user!
            var result = await _identityService.RegisterUserAsync(model, AccountType.Doctor, imagePath);

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

        public async Task<IdentityResult> EditDoctorAsync(DoctorUpdateModel model, string? imagePath)
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
            _unitOfWork.Complete();

            // To edit doctor's user account!
            var result = await _identityService.UpdateUserAsync(model, imagePath, doctor.User);

            return result;
        }

        public async Task<IdentityResult> DeleteDoctorAsync(int id)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetEntityByIdAsync(id, includeProperties: "User,Appointments");

            // Check if the doctor exists!
            if (doctor == null)
            {
                // Error of deleting failure (doctor is not found)!
                return HelpfulMessages.IdentityResultError($"Delete failed (Doctor of Id : {id} is not found)");
            }

            // Check if the doctor is booked!
            var bookings = await _unitOfWork.BookingRepository.GetAllAsync();

            if (bookings.Any())
            {
                var isBooked = bookings.Any(b => b.Time.Appointment.DoctorId == id && b.Status != BookingStatus.Cancelled);

                if (isBooked)
                {
                    // Error of deleting failure (doctor is booked)!
                    return HelpfulMessages.IdentityResultError($"Delete failed (Doctor of Id : {id} is booked)");
                }
            }

            // To delete doctor's data!
            _unitOfWork.DoctorRepository.DeleteEntity(doctor);
            _unitOfWork.Complete();

            // To delete doctor's user account!
            var result = await _identityService.DeleteUserAsync(doctor.User);

            return result;
        }

        public async Task<IEnumerable<UserDto>> GetAllPatientsAsync(StringSearchModel searchModel)
        {
            // Retrieving all users from the database!
            var users = await _identityRepository.GetAllUsersAsync();

            // To filter patients from users!
            var patients = users.Where(u => u.AccountType == AccountType.Patient);

            // Create new list of doctorDto to retrieve the data!
            List<UserDto> result = new List<UserDto>();

            foreach (var patient in patients)
            {
                var patientDto = new UserDto
                {
                    ProfileImage = patient.ProfileImage,
                    FullName = patient.FullName,
                    Email = patient.Email,
                    Phone = patient.PhoneNumber,
                    Gender = patient.Gender.ToString(),
                    DateOfBirth = patient.DateOfBirth
                };

                result.Add(patientDto);
            }

            // To filter patients and support pagination!
            var itemsToSkip = (searchModel.PageNumber - 1) * searchModel.PageSize;

            var filteredResult = result
                .Where(d => d.FullName.Contains(searchModel.Search) || d.Email.Contains(searchModel.Search))
                .Skip(itemsToSkip)
                .Take(searchModel.PageSize);

            return filteredResult;
        }

        public async Task<PatientDto> GetPatientByIdAsync(string id)
        {
            // Retrieving the patient from the database!
            var patient = await _identityRepository.FindUserByIdAsync(id);

            if (patient == null)
            {
                return null;
            }

            // Create new userDto to save patient details!
            var details = new UserDto
            {
                ProfileImage = patient.ProfileImage,
                FullName = patient.FullName,
                Email = patient.Email,
                Phone = patient.PhoneNumber,
                Gender = patient.Gender.ToString(),
                DateOfBirth = patient.DateOfBirth
            };

            // Create new list of bookingRequestDto to retrieve requests data!
            List<BookingRequestDto> requsts = new List<BookingRequestDto>();

            // Retrieve all bookings of patient!
            var bookings = patient.Bookings;

            if (bookings != null)
            {
                foreach (var booking in bookings)
                {
                    var doctor = booking.Time.Appointment.Doctor;
                    var appointment = booking.Time.Appointment;

                    var request = new BookingRequestDto
                    {
                        DoctorImage = doctor.User.ProfileImage,
                        DoctorName = doctor.User.FullName,
                        SpetializationAr = doctor.Specialization.NameAr,
                        SpetializationEn = doctor.Specialization.NameEn,
                        Day = appointment.Day,
                        Time = booking.Time.DocTime,
                        Price = booking.Price,
                        DiscountCode = booking.DiscountCode.Code,
                        FinalPrice = booking.FinalPrice,
                        Status = booking.Status
                    };

                    requsts.Add(request);
                }
            }

            // Create new patientDto to retrieve the data!
            var patientDto = new PatientDto
            {
                Details = details,
                Requests = requsts
            };

            return patientDto;
        }

    }
}
