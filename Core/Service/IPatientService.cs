using Core.Domain.DomainUtil;
using Core.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IPatientService
    {
        Task<IdentityResult> RegisterPatientAsync(UserRegisterModel model, string? imagePath);
    }
}
