using Core.Domain;
using Core.Domain.DomainUtil;
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
    }
}
