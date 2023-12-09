using Core.Domain.DomainUtil;
using Core.Model;
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

        public PatientService(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<IdentityResult> RegisterPatientAsync(UserRegisterModel model, string? imagePath)
        {
            return await _identityService.RegisterUserAsync(model, AccountType.Patient, imagePath);
        }
    }
}
