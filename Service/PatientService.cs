using Core.Domain.DomainUtil;
using Core.Model.DTOs;
using Core.Model.SearchModels;
using Core.Model.UserModels;
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
    public class PatientService : IPatientService
    {
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IIdentityService identityService, IUnitOfWork unitOfWork)
        {
            _identityService = identityService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> RegisterPatientAsync(UserRegisterModel model, string? imagePath)
        {
            return await _identityService.RegisterUserAsync(model, AccountType.Patient, imagePath);
        }

        public async Task<IEnumerable<DoctorPatientDto>> GetAllDoctorsAsync(StringSearchModel searchModel)
        {
            // Retrieving all doctors from the database!
            var doctors = await _unitOfWork.DoctorRepository.GetAllAsync(includeProperties: "User,Specialization,Appointments");

            // Create new list of doctorDto to retrieve the data!
            List<DoctorPatientDto> result = new List<DoctorPatientDto>();

            foreach (var doctor in doctors)
            {
                // To get appointments and times!
                List<DayDto> dayDtos = new List<DayDto>();

                foreach (var appointment in doctor.Appointments)
                {
                    // To get list of times!
                    List<TimeDto> timeDtos = new List<TimeDto>();
                    var times = (await _unitOfWork.TimeRepository.GetAllAsync())
                        .Where(t => t.AppointmentId == appointment.Id);

                    foreach (var time in times)
                    {
                        var timeDto = new TimeDto
                        {
                            Id = time.Id,
                            Time = time.DocTime
                        };

                        timeDtos.Add(timeDto);
                    }

                    // Add the list of times to their days!
                    var dayDto = new DayDto
                    {
                        Day = appointment.Day.ToString(),
                        Times = timeDtos
                    };

                    dayDtos.Add(dayDto);
                }

                // Insert doctor's fundamental data and his/her appointments list!
                var doctorPatientDto = new DoctorPatientDto
                {
                    ProfileImage = doctor.User.ProfileImage,
                    FullName = doctor.User.FullName,
                    Email = doctor.User.Email,
                    Phone = doctor.User.PhoneNumber,
                    Gender = doctor.User.Gender.ToString(),
                    SpetializationAr = doctor.Specialization.NameAr,
                    SpetializationEn = doctor.Specialization.NameEn,
                    Price = doctor.Price,
                    Appointments = dayDtos
                };

                result.Add(doctorPatientDto);
            }

            // To filter doctors and support pagination!
            var itemsToSkip = (searchModel.PageNumber - 1) * searchModel.PageSize;

            var filteredResult = result
                .Where(d => d.FullName.Contains(searchModel.Search) || d.Email.Contains(searchModel.Search))
                .Skip(itemsToSkip)
                .Take(searchModel.PageSize);

            return filteredResult;
        }

    }
}
